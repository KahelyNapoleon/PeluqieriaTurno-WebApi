using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.TipoServicioDTOs;

namespace PeluqueriaTurnoWebApi.Mappings
{
    public static class TipoServicioMapping
    {
        public static TipoServicioReadDTO ToReadDto(this TipoServicio t)
        {
            return new TipoServicioReadDTO
            {
                TipoServicioId = t.TipoServicioId,
                Descripcion = t.Descripcion
            };
        }

        public static TipoServicio ToEntity(this TipoServicioCreateUpdateDTO t)
        {
            return new TipoServicio
            {
                Descripcion = t.Descripcion
            };

        }
    }
}
