using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.TurnoDTOs;
using Contracts.DTOs.TurnoServicioDTOs;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TurnoServicioService
        (ITurnoServicioRepository turnoServicioRepository,
        IValidator<TurnoServicioCreateUpdateDTO> validator,
        IMappingService<TurnoServicio, TurnoServicioReadDTO, TurnoServicioCreateUpdateDTO> mapper)
        : GenericService<TurnoServicio, TurnoServicioReadDTO, TurnoServicioCreateUpdateDTO>(turnoServicioRepository, validator, mapper), ITurnoServicioService
    {
    }
}
