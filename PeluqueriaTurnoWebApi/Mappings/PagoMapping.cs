using DomainLayer.Models;
using PeluqueriaTurnoWebApi.DTOs.PagoDTOs;

namespace PeluqueriaTurnoWebApi.Mappings
{
    public static class PagoMapping
    {
        public static PagoReadDTO ToReadDTO(this Pago p)
        {
            return new PagoReadDTO
            {
                PagoId = p.PagoId,
                TurnoId = p.TurnoId,
                MetodoPagoId = p.MetodoPagoId,
                MontoTotal = p.MontoTotal,
                FechaPago = p.FechaPago
            };
        }

        public static Pago ToCreateEntity(this PagoCreateDTO p)
        {
            return new Pago
            {
                TurnoId = p.TurnoId,
                MetodoPagoId = p.MetodoPagoId,
                MontoTotal = p.MontoTotal,
                FechaPago = p.FechaPago
            };
        }

        public static Pago ToUpdateEntity(this PagoUpdateDTO p)
        {
            return new Pago
            {
                MetodoPagoId = p.MetodoPagoId,
                MontoTotal = p.MontoTotal,
                FechaPago = p.FechaPago
            };
        }
    }
}
