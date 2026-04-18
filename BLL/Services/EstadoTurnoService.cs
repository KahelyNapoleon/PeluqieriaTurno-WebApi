using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.EstadoTurnoDTOs;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using FluentValidation;
using System;

namespace BLL.Services
{
    public class EstadoTurnoService : GenericService<EstadoTurno, EstadoTurnoReadDTO, EstadoTurnoCreateUpdateDTO>, IEstadoTurnoService
    {
        private readonly IEstadoTurnoRepository _estadoTurnoRepository;
        private readonly IValidator<EstadoTurnoCreateUpdateDTO> _validator;
        private readonly IMappingService<EstadoTurno, EstadoTurnoReadDTO, EstadoTurnoCreateUpdateDTO> _mapper;

        public EstadoTurnoService(
            IEstadoTurnoRepository estadoTurnoRepository,
            IValidator<EstadoTurnoCreateUpdateDTO> validator,
            IMappingService<EstadoTurno, EstadoTurnoReadDTO, EstadoTurnoCreateUpdateDTO> mapper)
            : base(estadoTurnoRepository, validator, mapper)
        {
            _estadoTurnoRepository = estadoTurnoRepository ?? throw new ArgumentNullException(nameof(estadoTurnoRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
