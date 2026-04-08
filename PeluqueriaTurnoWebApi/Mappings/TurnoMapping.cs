using PeluqueriaTurnoWebApi.DTOs.TurnoDTOs;
using DomainLayer.Models;

namespace PeluqueriaTurnoWebApi.Mappings
{
    public static class TurnoMapping
    {
        public static TurnoReadDTO ToDTO(this Turno t)
        {
            return new TurnoReadDTO
            {
                TurnoId = t.TurnoId,
                Detalle = t.Detalle!,
                FechaTurno = t.FechaTurno,
                HoraTurno = t.HoraTurno,
                Cliente = t.Cliente.Nombre,
                EstadoTurno = t.EstadoTurno.Descripcion
            };
        }

    }
}
