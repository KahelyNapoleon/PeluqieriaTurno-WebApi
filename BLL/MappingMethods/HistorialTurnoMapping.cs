using BLL.Mapping;
using Contracts.DTOs.HistorialTurnoDTOs;
using DomainLayer.Models;


namespace BLL.MappingMethods
{
    public class HistorialTurnoMapping : IMappingService<HistorialTurno, HistorialTurnoReadDTO, HistorialTurnoCreateUpdateDTO>
    {

        public HistorialTurnoReadDTO ToReadDTO(HistorialTurno h)
        {
            return new HistorialTurnoReadDTO
            {
                HistorialTurnoId = h.HistorialTurnoId,
                FechaHoraActual = h.FechaHoraActual,
                FechaHoraAnterior = h.FechaHoraAnterior,
                EstadoTurnoActual = h.EstadoTurnoActual,
                EstadoTurnoAnterior = h.EstadoTurnoAnterior
            };
        }

        public void UpdateEntity(HistorialTurnoCreateUpdateDTO dto, HistorialTurno h)
        {
            h.FechaHoraActual = dto.FechaHoraActual;
            h.FechaHoraAnterior = dto.FechaHoraAnterior;
            h.EstadoTurnoAnterior = dto.EstadoTurnoAnterior;
            h.EstadoTurnoActual = dto.EstadoTurnoActual;
        }

        public HistorialTurno ToEntity(HistorialTurnoCreateUpdateDTO h)
        {
            return new HistorialTurno
            {
                FechaHoraActual = h.FechaHoraActual,
                FechaHoraAnterior = h.FechaHoraAnterior,
                EstadoTurnoActual = h.EstadoTurnoActual,
                EstadoTurnoAnterior = h.EstadoTurnoAnterior
            };
        }

    }
}
