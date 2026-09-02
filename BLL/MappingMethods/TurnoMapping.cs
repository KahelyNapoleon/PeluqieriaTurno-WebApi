using BLL.Mapping;
using Contracts.DTOs.TurnoDTOs;
using DomainLayer.Models;

namespace BLL.MappingMethods
{
    public class TurnoMapping : IMappingService<Turno, TurnoReadDTO, TurnoCreateUpdateDTO>
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
                EstadoTurnoDetalle = t.EstadoTurno.Descripcion,
                ServiciosId = t.TurnoServicios.Select(ts => ts.ServicioId)
                //Corregir aca ya que no hay coincidencia del tipo de datos. TurnoServicios es TurnoServicioReadDTO y t.TurnoServicios es de DomainModel

            };
        }
        public void UpdateEntity(TurnoCreateUpdateDTO dto, Turno t)
        {
            t.Detalle = dto.Detalle;
            t.FechaTurno = dto.FechaTurno;
            t.HoraTurno = dto.HoraTurno;
            t.ClienteId = dto.ClienteId;
            t.EstadoTurnoId = dto.EstadoTurnoId;
        }
        public Turno ToEntity(TurnoCreateUpdateDTO t)
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
