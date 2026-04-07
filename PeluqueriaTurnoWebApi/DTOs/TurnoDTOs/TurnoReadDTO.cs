namespace PeluqueriaTurnoWebApi.DTOs.TurnoDTOs
{
    public class TurnoReadDTO
    {
        public int TurnoId { get; set; }
        public string Detalle { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string EstadoTurno { get; set; } = null!;
        public DateOnly FechaTurno { get; set; }
        public TimeOnly HoraTurno { get; set; }
    }
}
