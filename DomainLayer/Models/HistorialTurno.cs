using System;
using System.Collections.Generic;

namespace DomainLayer.Models;

public partial class HistorialTurno
{
    
    public int HistorialTurnoId { get; set; }

    public int TurnoId { get; set; }

    public DateTimeOffset? FechaHoraAnterior { get; set; }

    public DateTimeOffset FechaHoraActual { get; set; }

    public int EstadoTurnoAnterior { get; set; } = default(int);

    public int EstadoTurnoActual { get; set; }

    public virtual EstadoTurno EstadoTurnoActualNavigation { get; set; } = null!;

    public virtual EstadoTurno EstadoTurnoAnteriorNavigation { get; set; } = null!;

    public virtual Turno Turno { get; set; } = null!;
}
