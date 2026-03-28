namespace PeluqueriaTurnoWebApi.DTOs.TurnoServicioDTOs
{
    public class TurnoServicioReadDTO
    {
        public int TurnoServicioId { get; set; }

        public int TurnoId { get; set; }

        public int ServicioId { get; set; }

        public decimal MontoAplicado { get; set; }

        public int? TiempoAplicado { get; set; }
    }
}
