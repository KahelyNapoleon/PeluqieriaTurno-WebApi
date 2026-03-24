namespace PeluqueriaTurnoWebApi.DTOs.PagoDTOs
{
    public class PagoReadDTO
    {
        public int PagoId { get; set; }

        public int TurnoId { get; set; }

        public int MetodoPagoId { get; set; }

        public decimal MontoTotal { get; set; }

        public DateTimeOffset FechaPago { get; set; }
    }
}
