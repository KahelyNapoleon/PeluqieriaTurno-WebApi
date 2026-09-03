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
    public interface ITurnoService : IGenericService<Turno, TurnoReadDTO, TurnoCreateUpdateDTO>
    {
        //Cambiar la implemetnacion de este metodo en la clase correspondiente
        Task<Result<IEnumerable<TurnoReadDTO?>>> GetPaged(int pageNumber, int pageSize);

        new Task<Result<TurnoReadDTO>> Add(TurnoCreateUpdateDTO turno);

        Task<Result<TurnoReadDTO>> UpdateEstadoTurno(int TurnoId,int EstadoTurnoId);

        Task<Result<TurnoReadDTO>> UpdateFechaYHora(int TurnoId, DateOnly nuevaFecha, TimeOnly nuevaHora);

    }
}
