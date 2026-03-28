using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeluqueriaTurnoWebApi.DTOs.TurnoServicioDTOs
{
    public class TurnoServicioCreateDTO
    {
        public int TurnoId { get; set; }

        public int ServicioId { get; set; }

        public decimal MontoAplicado { get; set; }

        public int? TiempoAplicado { get; set; }
    }
}
