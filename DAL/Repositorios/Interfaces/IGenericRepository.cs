using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetAll();
        Task<T?> GetById(int id);
        Task Add(T TEntity);
        Task Update(int id,T TEntity);
        Task Remove(T TEntity);

    }
}
