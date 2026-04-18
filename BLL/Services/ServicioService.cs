using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.ServicioDTOs;
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
    public class ServicioService
        (IServicioRepository servicioRepository,
        IValidator<ServicioCreateUpdateDTO> validator,
        IMappingService<Servicio, ServicioReadDTO, ServicioCreateUpdateDTO> mapper) 
        : GenericService<Servicio, ServicioReadDTO, ServicioCreateUpdateDTO>
        (servicioRepository, validator, mapper), IServicioService
    {
    }
}
