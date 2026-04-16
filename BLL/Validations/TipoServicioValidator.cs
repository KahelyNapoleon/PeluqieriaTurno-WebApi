using Contracts.DTOs.TipoServicioDTOs;
using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validations
{
    public class TipoServicioValidator : AbstractValidator<TipoServicioCreateUpdateDTO>
    {
        public TipoServicioValidator()
        {
            RuleFor(t => t.Descripcion).NotNull();
        }
    }
}
