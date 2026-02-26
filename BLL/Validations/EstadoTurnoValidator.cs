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
    public class EstadoTurnoValidator : AbstractValidator<EstadoTurno>
    {
        public EstadoTurnoValidator()
        {
            RuleFor(e => e.Descripcion).NotNull();
        }
    }
}
