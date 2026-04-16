using BLL.Mapping;
using Contracts.DTOs.TurnoDTOs;
using DomainLayer.Models;

namespace BLL.MappingMethods
{
    public class TurnoMapping : IMappingService<Turno, TurnoReadDTO, TurnoUpdateCreateDTO>
    {
        public TurnoReadDTO ToReadDTO(Turno t)
        {
            return new TurnoReadDTO
            {
                TurnoId = t.TurnoId,
                Detalle = t.Detalle!,
                FechaTurno = t.FechaTurno,
                HoraTurno = t.HoraTurno,
                ClienteId = t.ClienteId,
                EstadoTurno = t.EstadoTurno.Descripcion
            };
        }

        public void UpdateEntity(TurnoUpdateCreateDTO dto, Turno t)
        {
            t.Detalle = dto.Detalle;
            t.FechaTurno = dto.FechaTurno;
            t.HoraTurno = dto.HoraTurno;
            t.ClienteId = dto.ClienteId;
            t.EstadoTurnoId = dto.EstadoTurnoId;
        }

        public Turno ToEntity(TurnoUpdateCreateDTO t)
        {
            return new Turno
            {
                Detalle = t.Detalle,
                ClienteId = t.ClienteId,
                EstadoTurnoId = t.EstadoTurnoId,
                HoraTurno = t.HoraTurno,
                FechaTurno = t.FechaTurno
            };
        }

    }
}
