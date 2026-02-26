using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;
using DomainLayer.Models;
using FluentValidation;

namespace BLL.Validations
{
    public class HistorialTurnoValidator : AbstractValidator<HistorialTurno>
    {
        public HistorialTurnoValidator()
        {
            RuleFor(h => h.TurnoId).NotNull().GreaterThan(0);
            RuleFor(h => h.FechaHoraAnterior).NotNull().GreaterThan(DateTime.Now);
            RuleFor(h => h.FechaHoraActual).NotNull().GreaterThan(DateTime.Now);
            RuleFor(h => h.EstadoTurnoAnterior).NotNull().GreaterThan(0);
            RuleFor(h => h.EstadoTurnoActual).NotNull().GreaterThan(0);
        }
    }
}
