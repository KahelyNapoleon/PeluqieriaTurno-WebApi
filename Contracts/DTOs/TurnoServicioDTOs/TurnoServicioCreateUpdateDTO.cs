
namespace Contracts.DTOs.TurnoServicioDTOs
{
    public class TurnoServicioCreateUpdateDTO
    {
        public int TurnoId { get; set; }

        public int ServicioId { get; set; }

        public decimal MontoAplicado { get; set; }

        public int? TiempoAplicado { get; set; }
    }
}
