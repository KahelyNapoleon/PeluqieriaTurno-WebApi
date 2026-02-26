using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class MetodoPagoValidator : AbstractValidator<MetodoPago>
    {
        public MetodoPagoValidator()
        {
            RuleFor(m => m.Descripcion).NotNull();
        }
    }
}
