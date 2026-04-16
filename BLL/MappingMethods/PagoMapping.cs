using BLL.Mapping;
using Contracts.DTOs.PagoDTOs;
using DomainLayer.Models;


namespace BLL.MappingMethods
{
    public class PagoMapping : IMappingService<Pago, PagoReadDTO, PagoCreateUpdateDTO>
    {
        public  PagoReadDTO ToReadDTO( Pago p)
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

        public Pago ToEntity( PagoCreateUpdateDTO p)
        {
            return new Pago
            {
                TurnoId = p.TurnoId,
                MetodoPagoId = p.MetodoPagoId,
                MontoTotal = p.MontoTotal,
                FechaPago = p.FechaPago
            };
        }

        public void UpdateEntity( PagoCreateUpdateDTO dto, Pago p)
        {
            p.MetodoPagoId = dto.MetodoPagoId;
            p.MontoTotal = dto.MontoTotal;
            p.FechaPago = dto.FechaPago;

        }
    }
}
