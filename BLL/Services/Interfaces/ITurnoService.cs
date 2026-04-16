using BLL.Result;
using Contracts.DTOs.TurnoDTOs;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITurnoService : IGenericService<Turno, TurnoReadDTO, TurnoUpdateCreateDTO>
    {
        Task<Result<IEnumerable<Turno?>>> GetPaged(int pageNumber, int pageSize);
    }
}
