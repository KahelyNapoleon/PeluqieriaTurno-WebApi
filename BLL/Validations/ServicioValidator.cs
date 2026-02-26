using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class ServicioValidator : AbstractValidator<Servicio>
    {
        public ServicioValidator()
        {
            RuleFor(s => s.Descripcion).NotNull().NotEmpty();
            RuleFor(s => s.Precio).NotEmpty().GreaterThanOrEqualTo(10000.00m);
            RuleFor(s => s.Duracion).NotNull().GreaterThanOrEqualTo(30);
            RuleFor(s => s.Observacion).NotEmpty();
            RuleFor(s => s.TipoServicioId).NotNull().GreaterThan(0);
        }
    }
}
