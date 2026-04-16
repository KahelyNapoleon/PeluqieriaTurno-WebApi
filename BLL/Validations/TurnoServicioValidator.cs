using Contracts.DTOs.TurnoServicioDTOs;
using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class TurnoServicioValidator : AbstractValidator<TurnoServicioCreateUpdateDTO>
    {
        public TurnoServicioValidator()
        {
            RuleFor(ts => ts.TurnoId).NotNull().GreaterThan(0);
            RuleFor(ts => ts.ServicioId).NotNull().GreaterThan(0);
            RuleFor(ts => ts.MontoAplicado).NotEmpty().GreaterThanOrEqualTo(10000.00m);
            RuleFor(ts => ts.TiempoAplicado).NotEmpty().GreaterThanOrEqualTo(30);
        }
    }
}
