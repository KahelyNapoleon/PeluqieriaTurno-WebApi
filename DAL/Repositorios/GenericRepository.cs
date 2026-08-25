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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
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
            _dbContext.Entry(TEntity).State = EntityState.Added;
          
            await SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T?>> GetAll()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                return entities;
            }
            catch (SqlException ex)
            {
                var message = ex.InnerException!.Message;
             
                throw new ArgumentException(message);
            }
            
        }

        public virtual async Task<T?> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity!;
        }

        public virtual async Task Remove(T TEntity)
        {
            _dbContext.Entry(TEntity).State = EntityState.Deleted;
            await SaveChangesAsync();
        }

        public virtual async Task Update(int id, T TEntity)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbContext.Entry(entity!).CurrentValues.SetValues(TEntity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
