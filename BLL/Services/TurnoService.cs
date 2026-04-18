using BLL.Result;
using BLL.Services.Interfaces;
using DAL.Repositorios.Interfaces;
using DAL.UnitOfWork.IUnitOfWork;
using DomainLayer.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.TurnoDTOs;
using Contracts.DTOs.HistorialTurnoDTOs;
using BLL.Mapping;

namespace BLL.Services
{
    public class TurnoService : ITurnoService
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TurnoCreateUpdateDTO> _validatorTurno;
        private readonly IValidator<HistorialTurnoCreateUpdateDTO> _validatorHistorialTurno;
        private readonly IMappingService<Turno, TurnoReadDTO, TurnoCreateUpdateDTO> _mapper;

        public TurnoService(IUnitOfWork unitOfWork,
            IValidator<TurnoCreateUpdateDTO> validatorTurno,
            IValidator<HistorialTurnoCreateUpdateDTO> validatorHistorialTurno,
            IMappingService<Turno, TurnoReadDTO, TurnoCreateUpdateDTO> mapper
            )
        {
            _unitOfWork = unitOfWork;
            _validatorTurno = validatorTurno;
            _validatorHistorialTurno = validatorHistorialTurno;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TurnoReadDTO?>>> GetPaged(int pageNumber, int pageSize)
        {
            var result = await _unitOfWork.TurnoRepository.GetPaged(pageNumber, pageSize);
            if (!result.Any())
            {
                return Result<IEnumerable<Turno?>>.Fail("Aun no hay registros de turnos.");
            }

            var turnosDTO = result.Select(t => _mapper.ToReadDTO(t));

            return Result<IEnumerable<TurnoReadDTO?>>.Succes(turnosDTO);
        }

        //>>>>>>>>>>>>>CORREGIR TODO DE ACA HACIA ABAJO Y REVEER LO DE ARRIBA<<<<<<<<<<<<<<<<<<<<<<<<<

        public async Task<Result<IEnumerable<Turno?>>> GetAll()
        {
            var turnos = await _unitOfWork.TurnoRepository.GetAll();
            if (!turnos.Any())
            {
                return Result<IEnumerable<Turno?>>.Fail("Error, aun no hay registros de Turnos");
            }

            return Result<IEnumerable<Turno?>>.Succes(turnos);
        }

        public async Task<Result<Turno>> GetById(int id)
        {
            var turno = await _unitOfWork.TurnoRepository.GetById(id);
            if (turno == null)
            {
                return Result<Turno>.Fail($"No existe registro con id {id}");
            }

            return Result<Turno>.Succes(turno);
        }

        public async Task<Result<Turno>> Add(Turno turno)
        {
            await _unitOfWork.BeginTransactionAsync();

            var validarturno = await _validatorTurno.ValidateAsync(turno);

            if (!validarturno.IsValid)
            {
                return Result<Turno>.Fail(validarturno.Errors.ToString()!);
            }

            await _unitOfWork.TurnoRepository.Add(turno);
            await _unitOfWork.SaveChangeAsync();

            int estadoTurnoDisponible = 0;
            //inicia el historial
            var inicioHistorialTurno = new HistorialTurno
            {
                TurnoId = turno.TurnoId,
                FechaHoraAnterior = null,
                FechaHoraActual = new DateTimeOffset(turno.FechaTurno, turno.HoraTurno, TimeSpan.FromHours(-3)),
                EstadoTurnoAnterior = estadoTurnoDisponible,
                EstadoTurnoActual = turno.EstadoTurnoId,
            };

            //Es necesario validar en el propio codigo donde se valido el turno anteriormente?
            var validarHistorialTurno = await _validatorHistorialTurno.ValidateAsync(inicioHistorialTurno);
            if (!validarHistorialTurno.IsValid)
            {
                return Result<Turno>.Fail($"Algo salio mal en la validacion, {validarHistorialTurno.Errors}");
            }

            await _unitOfWork.HistorialTurnoRepository.Add(inicioHistorialTurno);
            await _unitOfWork.SaveChangeAsync();

            await _unitOfWork.CommitAsync();

            return Result<Turno>.Succes(turno);

        }

        public async Task<Result<Turno>> Update(int id, Turno turno)
        {
            await _unitOfWork.BeginTransactionAsync();

            //Recuperar los datos del turno antes de actualizar.
            var turnoAnterior = await _unitOfWork.TurnoRepository.GetById(id);
            if (turnoAnterior == null)
            {
                return Result<Turno>.Fail($"No existe registro con id {id}");
            }

            var validarTurno = await _validatorTurno.ValidateAsync(turno);
            if (!validarTurno.IsValid)
            {
                return Result<Turno>.Fail($"Error de validacion, {validarTurno.Errors}");
            }

            //Actualizar Turno
            await _unitOfWork.TurnoRepository.Update(id, turno);
            await _unitOfWork.SaveChangeAsync();

            //Agregar nuevo HistorialTurno de turno.
            //Registro que se conserva para el nuevo registor: Turno.
            //Registros Anteriores y Registros Actuales: EstadoTurno, FechaHora
            //         
            var fechaHoraAnterior = new DateTimeOffset(turnoAnterior.FechaTurno, turnoAnterior.HoraTurno, new TimeSpan(-3));
            var fechaHoraActual = new DateTimeOffset(turno.FechaTurno, turno.HoraTurno, new TimeSpan(-3));
            var estadoTurnoAnterior = turnoAnterior.EstadoTurnoId;
            var estadoTurnoActual = turno.EstadoTurnoId;

            var historialTurno = new HistorialTurno
            {
                TurnoId = turno.TurnoId,
                FechaHoraAnterior = fechaHoraAnterior,
                FechaHoraActual = fechaHoraActual,
                EstadoTurnoAnterior = estadoTurnoAnterior,
                EstadoTurnoActual = estadoTurnoActual

            };

            //Agregar nuevo registro de historial turno.
            await _unitOfWork.HistorialTurnoRepository.Add(historialTurno);
            await _unitOfWork.SaveChangeAsync();

            await _unitOfWork.CommitAsync();

            return Result<Turno>.Succes(turno);
        }

        public async Task<Result<string>> Delete(int id)
        {
            var eliminarTurno = await _unitOfWork.TurnoRepository.GetById(id);
            if (eliminarTurno == null)
            {
                return Result<string>.Fail($"Registro de Turno {id} no existe.");
            }

            await _unitOfWork.TurnoRepository.Remove(eliminarTurno);

            return Result<string>.Succes("Turno eliminado.");
        }


    }
}
