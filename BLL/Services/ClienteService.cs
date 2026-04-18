using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.ClienteDTOs;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using FluentValidation;
using System;

namespace BLL.Services
{
    public class ClienteService :
        GenericService<Cliente, ClienteReadDTO, ClienteCreateUpdateDTO>,
        IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(
            IClienteRepository clienteRepository,
            IValidator<ClienteCreateUpdateDTO> validator,
            IMappingService<Cliente, ClienteReadDTO, ClienteCreateUpdateDTO> mapper)
            : base(clienteRepository, validator, mapper)
        {
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
            _ = validator ?? throw new ArgumentNullException(nameof(validator));
            _ = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        
    }
}
