using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Result;
using BLL.Services.Interfaces;
using DAL.Repositorios;
using DAL.Repositorios.Interfaces;
using FluentValidation;

namespace BLL.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private IGenericRepository<T> _repository;
        private IValidator<T> _validator;
        public GenericService(IGenericRepository<T> repository, IValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public virtual async Task<Result<T>> Add(T TEntity)
        {
            var validationResult = await _validator.ValidateAsync(TEntity);
            if (!validationResult.IsValid)
            {
                return Result<T>.Fail(validationResult.Errors.ToString()!);
            }

            await _repository.Add(TEntity);
            return Result<T>.Succes(TEntity);

        }

        public virtual async Task<Result<string>> Delete(int id)
        {
            var TEntity = await _repository.GetById(id);
            if (TEntity == null)
            {
                return Result<string>.Fail($"El id {id} no existe.");
            }
            await _repository.Remove(TEntity);

            return Result<string>.Succes("Registro eliminado");
        }

        public virtual async Task<Result<IEnumerable<T>>> GetAll()
        {
            var TEntities = await _repository.GetAll();
            if (!TEntities.Any())
            {
                return Result<IEnumerable<T>>.Fail("Aun no hay registros.");
            }

            return Result<IEnumerable<T>>.Succes(TEntities);
        }

        public virtual async Task<Result<T>> GetById(int id)
        {
            var TEntity = await _repository.GetById(id);
            if (TEntity == null)
            {
                return Result<T>.Fail($"Registro con id {id} no se encuentra.");
            }

            return Result<T>.Succes(TEntity);
        }

        public virtual async Task<Result<T>> Update(int id, T TEntity)
        {
            // Validamos que el tipo de entrada Id exista.
            var entityExiste = await _repository.GetById(id);
            if (entityExiste == null)
            {
                return Result<T>.Fail("Id invalido, no existe en los registros");
            }

            //Validamos que los datos ingresados a TEntity sean correctos
            var validationResult = await _validator.ValidateAsync(TEntity);
            if (!validationResult.IsValid)
            {
                return Result<T>.Fail(validationResult.Errors.ToString()!);
            }

            //el id para que el repo busque la entidad y a partir de ahi reemplace con TEntity.
            await _repository.Update(id ,TEntity);

            return Result<T>.Succes(entityExiste);        
        }
    }
}
