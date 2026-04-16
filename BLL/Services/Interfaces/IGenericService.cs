using BLL.Mapping;
using BLL.Result;
using Contracts.DTOs.TurnoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IGenericService<Entity, TReadDTO, TCreateUpdateDTO> where Entity : class
    {
        Task<Result<IEnumerable<TReadDTO>>> GetAll();
        Task<Result<TReadDTO>> GetById(int id);
        Task<Result<TReadDTO>> Add(TCreateUpdateDTO entity);
        Task<Result<TCreateUpdateDTO>> Update(int id, TCreateUpdateDTO entity);
        Task<Result<string>> Delete(int id);

        //
    }
}
