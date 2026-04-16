
using BLL.Mapping;
using Contracts.DTOs.TurnoDTOs;
using Contracts.DTOs.TurnoServicioDTOs;
using DomainLayer.Models;

namespace BLL.MappingMethods
{
    public  class TurnoServicioMapping : IMappingService<TurnoServicio, TurnoServicioReadDTO, TurnoServicioCreateUpdateDTO>
    {
        public  TurnoServicioReadDTO ToReadDTO( TurnoServicio t)
        { 
            return new TurnoServicioReadDTO
            {
                TurnoServicioId = t.TurnoServicioId,
                TurnoId     = t.TurnoId,
                ServicioId = t.ServicioId,
                MontoAplicado = t.MontoAplicado,
                TiempoAplicado = t.TiempoAplicado,
            };
        }

        public void UpdateEntity(TurnoServicioCreateUpdateDTO dto, TurnoServicio t)
        {
            t.TurnoId = dto.TurnoId;
            t.ServicioId = dto.ServicioId;
            t.MontoAplicado = dto.MontoAplicado;
            t.TiempoAplicado = dto.TiempoAplicado;
        }

        public  TurnoServicio ToEntity( TurnoServicioCreateUpdateDTO t)
        {
            return new TurnoServicio
            {
                TurnoId = t.TurnoId,
                ServicioId = t.ServicioId,
                MontoAplicado = t.MontoAplicado,
                TiempoAplicado = t.TiempoAplicado

            };
        }

     
    }
}
