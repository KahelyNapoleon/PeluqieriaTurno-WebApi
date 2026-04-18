using BLL.Mapping;
using BLL.Services.Interfaces;
using Contracts.DTOs.HistorialTurnoDTOs;
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
    public class HistorialTurnoService : GenericService<HistorialTurno, HistorialTurnoReadDTO, HistorialTurnoCreateUpdateDTO>, IHistorialTurnoService
    {
        public HistorialTurnoService(
            IHistorialTurnoRepository historialTurnoRepository,
            IValidator<HistorialTurnoCreateUpdateDTO> validator,
            IMappingService<HistorialTurno, HistorialTurnoReadDTO, HistorialTurnoCreateUpdateDTO> mapper
        ) : base(historialTurnoRepository, validator, mapper)
        {
        }
    }
}
