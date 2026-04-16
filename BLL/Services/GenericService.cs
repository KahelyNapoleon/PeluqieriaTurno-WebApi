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
    public class GenericService<Entity, TReadDTO, TCreateUpdateDTO> : IGenericService<Entity, TReadDTO, TCreateUpdateDTO> where Entity : class
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

        /*
         PSEUDOCÓDIGO (plan detallado):
         1. Validar que el DTO de entrada no sea nulo; lanzar ArgumentNullException si lo es.
         2. Ejecutar la validación fluida (_validator) sobre el DTO de creación/actualización.
            - Si no es válido, construir un mensaje de error concatenando los mensajes de cada Error.
            - Retornar Result<TReadDTO>.Fail(...) con ese mensaje.
         3. Convertir el DTO de entrada a la entidad con _mapper.ToEntity.
         4. Llamar a _repository.Add(entity) y esperar a que termine.
            - Asumimos que el repositorio (p. ej. EF Core) actualizará la entidad con el Id generado.
         5. Convertir la entidad actualizada (que contiene el Id) a TReadDTO mediante _mapper.ToReadDto.
         6. Retornar Result<TReadDTO>.Succes(readDto) para devolver el DTO de lectura con el Id generado.
        */

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
            var readDto = _mapper.ToReadDto(entity);

            return Result<TReadDTO>.Succes(readDto);
        }


        public virtual async Task<Result<string>> Delete(int id)
        {

            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                return Result<string>.Fail($"El id {id} no existe.");
            }
            await _repository.Remove(entity);

            return Result<string>.Succes("Registro eliminado");
        }

        public virtual async Task<Result<IEnumerable<TReadDTO>>> GetAll()
        {
            var entities = await _repository.GetAll();
            if (!entities.Any())
            {
                return Result<IEnumerable<TReadDTO>>.Fail("Aun no hay registros.");
            }

            var entitiesDto = entities.Select(e => _mapper.ToReadDTO(e!));

            return Result<IEnumerable<TReadDTO>>.Succes(entitiesDto);
        }

        public virtual async Task<Result<Entity>> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                return Result<Entity>.Fail($"Registro con id {id} no se encuentra.");
            }

            return Result<Entity>.Succes(entity);
        }

        public virtual async Task<Result<Entity>> Update(int id, Entity entity)
        {
            // Validamos que el tipo de entrada Id exista.
            var entityExiste = await _repository.GetById(id);
            if (entityExiste == null)
            {
                return Result<Entity>.Fail("Id invalido, no existe en los registros");
            }

            //Validamos que los datos ingresados a TEntity sean correctos
            var validationResult = await _validator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                return Result<Entity>.Fail(validationResult.Errors.ToString()!);
            }

            //el id para que el repo busque la entidad y a partir de ahi reemplace con TEntity.
            await _repository.Update(id, entity);

            return Result<Entity>.Succes(entityExiste);
        }
    }
}
