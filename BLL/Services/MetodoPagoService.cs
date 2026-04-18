using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.MetodoPagoDTOs;
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
    public class MetodoPagoService : GenericService<MetodoPago, MetodoPagoReadDTO, MetodoPagoCreateUpdateDTO >, IMetodoPagoService
    {
        public MetodoPagoService(
            IMetodoPagoRepository metodoPagoRepository,
            IValidator<MetodoPagoCreateUpdateDTO> validator,
            IMappingService<MetodoPago, MetodoPagoReadDTO, MetodoPagoCreateUpdateDTO> mapper
        ) : base(metodoPagoRepository, validator, mapper)
        {
        }
    }
}
