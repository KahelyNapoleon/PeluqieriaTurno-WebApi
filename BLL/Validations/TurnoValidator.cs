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
            RuleFor(t => t.EstadoTurnoId).NotNull().GreaterThan(0);
            RuleFor(t => t.HoraTurno).NotNull().GreaterThan(new TimeOnly(9,0));
            RuleFor(t => t.FechaTurno).NotNull().GreaterThan(DateOnly.FromDateTime(DateTime.Today));
            //Validador de servicios no repetidos
            RuleFor(t => t.Servicios)
                .NotNull()
                .Must( servicios => servicios //Must retorna true o false
                .Select(s => s.ServicioId) //selecciona cada servicioId de ICollection<servicio>
                .Distinct() //Elimina duplicados
                .Count() == servicios.Count ) // Compara que la cantidad
                               //Si se elimino un duplicado entonces hay un servicio repetido
                .WithMessage("No puede haber servicios repetidos en el turno.");
        }
    }
}
