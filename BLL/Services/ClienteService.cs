using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.ClienteDTOs;
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
    public class ClienteService
        (IClienteRepository clienteRepository,
        IValidator<ClienteCreateUpdateDTO> validator,
        IMappingService<Cliente,ClienteReadDTO,ClienteCreateUpdateDTO> mapper)
        : GenericService<Cliente,ClienteReadDTO,ClienteCreateUpdateDTO>(clienteRepository, validator, mapper) , IClienteService
    {
    }
}
