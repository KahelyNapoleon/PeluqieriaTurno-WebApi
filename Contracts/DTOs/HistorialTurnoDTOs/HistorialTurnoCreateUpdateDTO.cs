

namespace Contracts.DTOs.HistorialTurnoDTOs
{
    public class HistorialTurnoCreateUpdateDTO
    {
        public DateTimeOffset? FechaHoraAnterior { get; set; }

        public DateTimeOffset FechaHoraActual { get; set; }

        public int EstadoTurnoAnterior { get; set; }

        public int EstadoTurnoActual { get; set; }
    }
}
