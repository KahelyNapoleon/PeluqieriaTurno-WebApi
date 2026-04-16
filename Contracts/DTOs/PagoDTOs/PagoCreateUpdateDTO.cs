using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.PagoDTOs
{
    public class PagoCreateUpdateDTO
    {
        public int TurnoId { get; set; }

        public int MetodoPagoId { get; set; }

        public decimal MontoTotal { get; set; }

        public DateTimeOffset FechaPago { get; set; }
    }
}
