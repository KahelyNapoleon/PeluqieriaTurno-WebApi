using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.HistorialTurnoDTOs;

namespace PeluqueriaTurnoWebApi.Mappings
{
    internal static class HistorialTurnoMapping
    {

        public static HistorialTurnoReadDTO ToReadDTO(this HistorialTurno h)
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

        public static HistorialTurnoCreateUpdateDTO ToDto(this HistorialTurno h)
        {
            return new HistorialTurnoCreateUpdateDTO
            {
                FechaHoraActual = h.FechaHoraActual,
                FechaHoraAnterior = h.FechaHoraAnterior,
                EstadoTurnoActual = h.EstadoTurnoActual,
                EstadoTurnoAnterior = h.EstadoTurnoAnterior
            };
        }

        public static HistorialTurno ToEntity(this HistorialTurnoCreateUpdateDTO h)
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
