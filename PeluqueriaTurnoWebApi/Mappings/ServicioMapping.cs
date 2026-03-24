using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.ServicioDTOs;

namespace PeluqueriaTurnoWebApi.Mappings
{
    public static class ServicioMapping
    {
        public static ServicioReadDTO ToReadDto(this Servicio s)
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

        public static Servicio ToEntity(this ServicioCreateUpdateDTO s)
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
