using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mapping;
using BLL.Result;
using BLL.Services.Interfaces;
using DAL.Repositorios;
using DAL.Repositorios.Interfaces;
using FluentValidation;

namespace BLL.Services
{
    public class GenericService<Entity, TReadDTO, TCreateUpdateDTO>
        : IGenericService<Entity, TReadDTO, TCreateUpdateDTO> where Entity : class
                                                              where TReadDTO : class
                                                              where TCreateUpdateDTO : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IValidator<TCreateUpdateDTO> _validator;
        private readonly IMappingService<Entity, TReadDTO, TCreateUpdateDTO> _mapper;
        public GenericService(IGenericRepository<Entity> repository,
                              IValidator<TCreateUpdateDTO> validator,
                              IMappingService<Entity, TReadDTO, TCreateUpdateDTO> mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }




        public virtual async Task<Result<TReadDTO>> Add(TCreateUpdateDTO entityDto)
        {
            // Programación defensiva
            if (entityDto == null) throw new ArgumentNullException(nameof(entityDto));

            var validationResult = await _validator.ValidateAsync(entityDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<TReadDTO>.Fail(errors);
            }

            var entity = _mapper.ToEntity(entityDto);

            // Agregamos la entidad al repositorio; se espera que el repositorio asigne el Id
            await _repository.Add(entity);

            // Convertimos la entidad actualizada (con Id) al DTO de lectura y lo retornamos
            //PROBLEMA entity ES EL TIPO DE VALOR DE ENTRADA SIN ID ASIGNADO POR ENDE AQUI EL 
            //CODIGO TIENE UN ERROR
            //CORREGIR CORREGIR

            var readDto = _mapper.ToReadDTO(entity);

            return Result<TReadDTO>.Succes(readDto);
        }


        public virtual async Task<Result<string>> Delete(int id)
        {
            if (id <= 0) throw new ArgumentNullException("Id incorrecto");

            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                return Result<string>.Fail($"El id {id} no existe.");
            }
            await _repository.Remove(entity);

            return Result<string>.Succes("Registro eliminado");
        }

        public virtual async Task<Result<IEnumerable<TReadDTO?>>> GetAll()
        {
            var entities = await _repository.GetAll();
            if (!entities.Any())
            {
                return Result<IEnumerable<TReadDTO?>>.Fail("Aun no hay registros.");
            }

            var entitiesDto = entities.Select(e => _mapper.ToReadDTO(e!));

            return Result<IEnumerable<TReadDTO?>>.Succes(entitiesDto);
        }

        public virtual async Task<Result<TReadDTO>> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                return Result<TReadDTO>.Fail($"Registro con id {id} no se encuentra.");
            }

            var entityDto = _mapper.ToReadDTO(entity);

            return Result<TReadDTO>.Succes(entityDto);
        }

        public virtual async Task<Result<TReadDTO>> Update(int id, TCreateUpdateDTO entity)
        {
            if (id <= 0) throw new ArgumentNullException("Id incorrecto");
            //Validamos que los datos ingresados a TEntity sean correctos
            var validationResult = await _validator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<TReadDTO>.Fail(errors);
            }

            // Validamos que el registro de id existe realmente
            var entityExiste = await _repository.GetById(id);
            if (entityExiste == null)
            {
                return Result<TReadDTO>.Fail("Id incorrecto o inexistente");
            }

            //Realizo la conversion del valor de entrada entity que actualizar a los valores reales
            //Para luego poder ejecutar el metodo del repositorio de Update(id, entity)
            var toEntity = _mapper.ToEntity(entity);

            await _repository.Update(id, toEntity);

            var entityUpdate = await _repository.GetById(id);
            if (entityUpdate == null)
            {
                return Result<TReadDTO>.Fail("Error al recuperar el registro");
            }

            var entityUpdateDto = _mapper.ToReadDTO(entityUpdate);

            return Result<TReadDTO>.Succes(entityUpdateDto);
        }
    }
}
