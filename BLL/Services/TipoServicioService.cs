using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.TipoServicioDTOs;
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
    public class TipoServicioService
        (ITipoServicioRepository tipoServicioRepository,
        IValidator<TipoServicioCreateUpdateDTO> validator,
        IMappingService<TipoServicio, TipoServicioReadDTO, TipoServicioCreateUpdateDTO> mapper) 
        : GenericService<TipoServicio, TipoServicioReadDTO, TipoServicioCreateUpdateDTO>(tipoServicioRepository, validator, mapper)
        , ITipoServicioService
    {
    }
}
