using DAL.Data;
using DAL.Repositorios.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repositorios
{
    public class GenericRepository<T> : IGenericRepository<T> where T:class
    {
        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _dbContext = dbContext;
        }

        public virtual async Task Add(T TEntity)
        {
            try
            {
                _dbContext.Entry(TEntity).State = EntityState.Added;
                // await _dbSet.AddAsync(TEntity);
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var error = $"The entity {nameof(T)} could not be saved in the database.";
                var exceptionMessage = ex.InnerException!.Message;
                throw new DbUpdateConcurrencyException(exceptionMessage + error);
            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException!.Message;
                throw new Exception("Occurred a problem to access to database. " + message);
            }

           
        }

        public virtual async Task<IEnumerable<T?>> GetAll()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                return entities;
            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException!.Message;
                throw new Exception("Occurred a problem to access to database. "+message);
            }
        }

        public virtual async Task<T?> GetById(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                return entity!;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocurred a problem to access to database" +ex.InnerException!.Message);
            }
        }

        public virtual async Task Remove(T TEntity)
        {
            try
            {
                _dbContext.Entry(TEntity).State = EntityState.Deleted;
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("The entity doesn't exist. " + ex.InnerException!.Message);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("The entity can not be remove by restrictions." +ex.InnerException!.Message);
            }
        }

        /// <summary>
        /// Actualizar un registro de modelo de entidad
        /// </summary>
        /// <param name="id">Busca el registro de entidad de modelo</param>
        /// <param name="TEntity">De tipo DTO con las propiedades nuevas para actualizar
        /// el registro de entidad</param>
        /// <returns>No retorna nada, realiza tal cambio en la base de datos</returns>
        public virtual async Task Update(int id, T TEntity)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
            //Y que sucede si TEntity no es del mism otipo que entity?
            //EFCore resuelve esto, actualiza las propiedades, del modelo de entidad, de nombre
            //que coinciden con las del objeto de tipo DTO
            _dbContext.Entry(entity!).CurrentValues.SetValues(TEntity);
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("The entity doesn't exist or There was another process where modified" +ex.InnerException!.Message);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("The entity can not be update by restrictions." +ex.InnerException!.Message);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
