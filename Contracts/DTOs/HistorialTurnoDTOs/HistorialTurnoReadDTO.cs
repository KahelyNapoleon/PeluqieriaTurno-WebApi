namespace Contracts.DTOs.HistorialTurnoDTOs
{
    public class HistorialTurnoReadDTO
    {
        public int HistorialTurnoId { get; set; }
        public int TurnoId { get; set; }
        public DateTimeOffset? FechaHoraAnterior { get; set; }

        public DateTimeOffset FechaHoraActual { get; set; }

        public int EstadoTurnoAnterior { get; set; }

        public int EstadoTurnoActual { get; set; }
    }
}
