namespace PeluqueriaTurnoWebApi.DTOs.ServicioDTOs
{
    public class ServicioReadDTO
    {
        public int ServicioId { get; set; }

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int Duracion { get; set; }

        public string? Observacion { get; set; }

        public int TipoServicioId { get; set; }
    }
}
