using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.PagoDTOs;
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
    public class PagoService
        (IPagoRepository pagoRepository, IValidator<PagoCreateUpdateDTO> validator, IMappingService<Pago,PagoReadDTO,PagoCreateUpdateDTO> mapper)
        : GenericService<Pago, PagoReadDTO, PagoCreateUpdateDTO>(pagoRepository, validator, mapper),
        IPagoService
    {
    }
}
