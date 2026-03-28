using PeluqueriaTurnoWebApi.DTOs.TurnoServicioDTOs;
using DomainLayer.Models;

namespace PeluqueriaTurnoWebApi.Mappings
{
    public static class TurnoServicioMapping
    {
        public static TurnoServicioReadDTO ToReadDto(this TurnoServicio t)
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

        public static TurnoServicio ToCreateEntity(this TurnoServicioCreateDTO t)
        {
            return new TurnoServicio
            {
                TurnoId = t.TurnoId,
                ServicioId = t.ServicioId,
                MontoAplicado = t.MontoAplicado,
                TiempoAplicado = t.TiempoAplicado

            };
        }

        public static TurnoServicio ToUpdateEntity(this TurnoServicioUpdateDTO t)
        {
            return new TurnoServicio
            {
                ServicioId = t.ServicioId,
                MontoAplicado = t.MontoAplicado,
                TiempoAplicado = t.TiempoAplicado
            };
        }
    }
}
