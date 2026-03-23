using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.MetodoPagoDTOs;

namespace PeluqueriaTurnoWebApi.Mappings
{
    internal static class MetodoPagoMapping
    {
        public static MetodoPagoReadDTO ToReadDTO(this MetodoPago m)
        {
            return new MetodoPagoReadDTO
            {
                MetodoPagoId = m.MetodoPagoId,
                Descripcion = m.Descripcion,
            };
        }

        public static MetodoPagoCreateUpdateDTO ToDTO(this MetodoPago m)
        {
            return new MetodoPagoCreateUpdateDTO
            {
                Descripcion = m.Descripcion
            };
        }

        public static MetodoPago ToEntity(this MetodoPagoCreateUpdateDTO m)
        {
            return new MetodoPago
            {
                Descripcion = m.Descripcion
            };
        }
    }
}
