using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using DomainLayer;
using DomainLayer.Models;
using Contracts.DTOs.ClienteDTOs;

namespace BLL.Validations
{
    public class ClienteValidator : AbstractValidator<ClienteCreateUpdateDTO>
    {
        public ClienteValidator() 
        {
            RuleFor(c => c.Nombre).NotNull();
            RuleFor(c => c.Apellido).NotNull();
            RuleFor(c => c.NroCelular).NotNull();
            RuleFor(c => c.CorreoElectronico).EmailAddress();
            RuleFor(c => c.FechaNacimiento).NotNull();
            RuleFor(c => c.FechaNacimiento).NotEmpty().LessThan(DateOnly.FromDateTime(DateTime.Today));
            RuleFor(c => c.Preferencias).NotEmpty();
            RuleFor(c => c.Observaciones).NotEmpty();
            RuleFor(c => c.Activo).NotNull();
        }
    }
}
