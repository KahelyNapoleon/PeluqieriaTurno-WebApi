using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class PagoValidator : AbstractValidator<Pago>
    {
        public PagoValidator()
        {
            RuleFor(p => p.TurnoId).NotNull().GreaterThan(0);
            RuleFor(p => p.MetodoPagoId).NotNull().GreaterThan(0);
            RuleFor(p => p.MontoTotal).NotNull().GreaterThan(10000.00m);
            RuleFor(p => p.FechaPago).NotNull().GreaterThanOrEqualTo(DateTimeOffset.Now);
            
        }
    }
}
