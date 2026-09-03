using Contracts.DTOs.TurnoDTOs;
using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class TurnoValidator : AbstractValidator<TurnoCreateUpdateDTO>
    {
        public TurnoValidator()
        {
            RuleFor(t => t.Detalle).NotEmpty();
            RuleFor(t => t.ClienteId).NotNull().GreaterThan(0);
            RuleFor(t => t.EstadoTurnoId).NotNull().GreaterThan(0).LessThan(8);
            RuleFor(t => t.HoraTurno).NotNull()
                .GreaterThan(new TimeOnly(9,0)).WithMessage("La hora debe ser mayor a las 9:00am.")
                .LessThan(new TimeOnly(18,0)).WithMessage("La hora debe ser menor a las 18:00pm.");
            RuleFor(t => t.FechaTurno).NotNull().GreaterThan(DateOnly.FromDateTime(DateTime.Today));
            RuleFor(t => t.ServiciosId).NotNull().WithMessage("La lista no debe estar vacia.")
                .Must(ids => ids == null || ids.Distinct().Count() ==ids.Count())
                .WithMessage("Hay uno o mas servicios repetidos."); // Aca debo validar que no haya ids repetidos.
  
        }
    }
}
