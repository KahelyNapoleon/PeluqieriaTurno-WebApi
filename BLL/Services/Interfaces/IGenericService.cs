using BLL.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<Result<IEnumerable<T?>>> GetAll();
        Task<Result<T>> GetById(int id);
        Task<Result<T>> Add(T TEntity);
        Task<Result<T>> Update(int id, T TEntity);
        Task<Result<string>> Delete(int id);

        //
    }
}
