using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeluqueriaTurnoWebApi.DTOs.PagoDTOs
{
    public class PagoUpdateDTO
    {
        public int MetodoPagoId { get; set; }

        public decimal MontoTotal { get; set; }

        public DateTimeOffset FechaPago { get; set; }
    }
}
