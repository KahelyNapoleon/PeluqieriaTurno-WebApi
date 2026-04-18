using Contracts.DTOs.HistorialTurnoDTOs;
using DomainLayer.Models;


namespace BLL.Services.Interfaces
{
    public interface IHistorialTurnoService :
        IGenericService<HistorialTurno, HistorialTurnoReadDTO, HistorialTurnoCreateUpdateDTO>
    {
    }
}
