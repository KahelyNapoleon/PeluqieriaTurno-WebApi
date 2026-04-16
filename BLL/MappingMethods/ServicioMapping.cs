using DomainLayer.Models;
using Contracts.DTOs.ServicioDTOs;
using BLL.Mapping;

namespace BLL.MappingMethods
{
    public  class ServicioMapping : IMappingService<Servicio, ServicioReadDTO, ServicioCreateUpdateDTO>
    {
        public  ServicioReadDTO ToReadDTO( Servicio s)
        {
            return new ServicioReadDTO
            {
                ServicioId = s.ServicioId,
                Descripcion = s.Descripcion,
                Precio = s.Precio,
                Duracion = s.Duracion,
                Observacion = s.Observacion,
                TipoServicioId = s.TipoServicioId
            };
        }

        public void UpdateEntity(ServicioCreateUpdateDTO dto, Servicio s)
        {
            s.Descripcion = dto.Descripcion;
            s.Precio = dto.Precio;
            s.Duracion = dto.Duracion;
            s.Observacion = dto.Observacion;
            s.TipoServicioId= dto.TipoServicioId;
        }

        public  Servicio ToEntity( ServicioCreateUpdateDTO s)
        {
            return new Servicio
            {
                Descripcion = s.Descripcion,
                Precio = s.Precio,
                Duracion = s.Duracion,
                Observacion = s.Observacion,
                TipoServicioId = s.TipoServicioId
            };
        }
    }
}
