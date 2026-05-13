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
using Contracts.DTOs.TurnoServicioDTOs;
using System.Reflection.PortableExecutable;

namespace BLL.Services
{
    public class TurnoService : ITurnoService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TurnoCreateUpdateDTO> _validatorTurno;
        private readonly IValidator<HistorialTurnoCreateUpdateDTO> _validatorHistorialTurno;
        private readonly IValidator<TurnoServicioCreateUpdateDTO> _validatorTurnoServicio;
        private IMappingService<Turno, TurnoReadDTO, TurnoCreateUpdateDTO> _mapperTurno;
        private IMappingService<HistorialTurno, HistorialTurnoReadDTO, HistorialTurnoCreateUpdateDTO> _mapperHistorialTurno;
        private IMappingService<TurnoServicio, TurnoServicioReadDTO, TurnoServicioCreateUpdateDTO> _mapperTurnoServicio;

        public TurnoService(IUnitOfWork unitOfWork,
            IValidator<TurnoCreateUpdateDTO> validatorTurno,
            IValidator<HistorialTurnoCreateUpdateDTO> validatorHistorialTurno,
            IValidator<TurnoServicioCreateUpdateDTO> validatorTurnoServicio,
            IMappingService<Turno, TurnoReadDTO, TurnoCreateUpdateDTO> mapperTurno,
            IMappingService<HistorialTurno, HistorialTurnoReadDTO, HistorialTurnoCreateUpdateDTO> mapperHistorialTurno,
            IMappingService<TurnoServicio, TurnoServicioReadDTO, TurnoServicioCreateUpdateDTO> mapperTurnoServicio
            )
        {
            _unitOfWork = unitOfWork;
            _validatorTurno = validatorTurno;
            _validatorHistorialTurno = validatorHistorialTurno;
            _validatorTurnoServicio = validatorTurnoServicio;
            _mapperTurno = mapperTurno;
            _mapperHistorialTurno = mapperHistorialTurno;
            _mapperTurnoServicio = mapperTurnoServicio;
        }

        public async Task<Result<IEnumerable<TurnoReadDTO?>>> GetPaged(int pageNumber, int pageSize)
        {
            var result = await _unitOfWork.TurnoRepository.GetPaged(pageNumber, pageSize);
            if (!result.Any())
            {
                return Result<IEnumerable<TurnoReadDTO?>>.Fail("Aun no hay registros de turnos.");
            }

            var turnosDTO = result.Select(t => _mapperTurno.ToReadDTO(t!));

            return Result<IEnumerable<TurnoReadDTO?>>.Succes(turnosDTO);
        }

        //>>>>>>>>>>>>>CORREGIR TODO DE ACA HACIA ABAJO Y REVEER LO DE ARRIBA<<<<<<<<<<<<<<<<<<<<<<<<<

        public async Task<Result<IEnumerable<TurnoReadDTO?>>> GetAll()
        {
            var turnos = await _unitOfWork.TurnoRepository.GetAll();
            if (!turnos.Any())
            {
                return Result<IEnumerable<TurnoReadDTO?>>.Fail("Error, aun no hay registros de Turnos");
            }

            var turnosDTO = turnos.Select(t => _mapperTurno.ToReadDTO(t!));

            return Result<IEnumerable<TurnoReadDTO?>>.Succes(turnosDTO);
        }

        public async Task<Result<TurnoReadDTO>> GetById(int id)
        {
            var turno = await _unitOfWork.TurnoRepository.GetById(id);
            if (turno == null)
            {
                return Result<TurnoReadDTO>.Fail($"No existe registro con id {id}");
            }

            var turnoDTO = _mapperTurno.ToReadDTO(turno);

            return Result<TurnoReadDTO>.Succes(turnoDTO);
        }

        public async Task<Result<TurnoReadDTO>> Add(TurnoCreateUpdateDTO turno)
        {
            //if (turno == null) throw new ArgumentNullException("El registro de turno debe completarse.");

            await _unitOfWork.BeginTransactionAsync();


            var validarturno = await _validatorTurno.ValidateAsync(turno);

            if (!validarturno.IsValid)
            {
                var errors = string.Concat("; ", validarturno.Errors.Select(e => e));
                return Result<TurnoReadDTO>.Fail(errors);
            }

            //ENTIDAD PARA PASAR A PARAMETRO DE REPOSITORIO.
            var turnoToEntity = _mapperTurno.ToEntity(turno);

            await _unitOfWork.TurnoRepository.Add(turnoToEntity);
            await _unitOfWork.SaveChangeAsync();

            int estadoTurnoDisponible = 0;
            //inicia el historial
            var inicioHistorialTurno = new HistorialTurnoCreateUpdateDTO
            {
                TurnoId = turnoToEntity.TurnoId,
                FechaHoraAnterior = null,
                FechaHoraActual = new DateTimeOffset(turnoToEntity.FechaTurno, turnoToEntity.HoraTurno, TimeSpan.FromHours(-3)),
                EstadoTurnoAnterior = estadoTurnoDisponible,
                EstadoTurnoActual = turnoToEntity.EstadoTurnoId,
            };

            //Es necesario validar en el propio codigo donde se valido el turno anteriormente?
            var validarHistorialTurno = await _validatorHistorialTurno.ValidateAsync(inicioHistorialTurno);
            if (!validarHistorialTurno.IsValid)
            {
                var errors = string.Concat("; ", validarHistorialTurno.Errors.Select(e => e));
                return Result<TurnoReadDTO>.Fail(errors);
            }

            //MAPEO DE HISTORIALTURNO A ENTITY PARA PASAR A PARAMETRO DE REPOSITORIO 
            var historialTurnoToEntity = _mapperHistorialTurno.ToEntity(inicioHistorialTurno);

            await _unitOfWork.HistorialTurnoRepository.Add(historialTurnoToEntity);
            await _unitOfWork.SaveChangeAsync();


            //BLOQUE AGREGAR SERVICIOS A TABLA TurnoServicio
            //Crear Registros de los Servicios del turno
            //EN OBSERVACION
            var servicios = turno.Servicios;
            foreach (var servicio in servicios)
            {
                var agregarServicio = new TurnoServicioCreateUpdateDTO
                {
                    TurnoId = turnoToEntity.TurnoId,
                    ServicioId = servicio.ServicioId,
                    MontoAplicado = servicio.Precio,
                    TiempoAplicado = servicio.Duracion
                };

                //mapear turno servicio a Entity para poder ingresarlo al parametro de repositorio.
                var agregarServicioEntity = _mapperTurnoServicio.ToEntity(agregarServicio);

                await _unitOfWork.TurnoServicioRepository.Add(agregarServicioEntity);

                await _unitOfWork.SaveChangeAsync();
            }


            await _unitOfWork.CommitAsync();


            var turnoDto = _mapperTurno.ToReadDTO(turnoToEntity);

            return Result<TurnoReadDTO>.Succes(turnoDto);

        }


        //sI LO QUE QUIERO CONSEGUIR ES CAMBIAR EL ESTADO DEL TURNO, EL NOMBRE Y LA IMPLEMENTACION DEL METODO
        //DEBEN CAMBIAR Y SER DIFERENTE COMO UpdateEstadoTurno y recibir como parametro el estado del Turno actualizado.
        public async Task<Result<TurnoReadDTO>> Update(int id, TurnoCreateUpdateDTO turno)
        {
            if (turno == null) throw new ArgumentNullException("Los campos de Turno deben completarse.");

            await _unitOfWork.BeginTransactionAsync();

            //Recuperar los datos del turno antes de actualizar.
            var turnoAnterior = await _unitOfWork.TurnoRepository.GetById(id);
            if (turnoAnterior == null)
            {
                return Result<TurnoReadDTO>.Fail($"No existe registro con id {id}");
            }

            var validarTurno = await _validatorTurno.ValidateAsync(turno);
            if (!validarTurno.IsValid)
            {
                var errors = string.Concat("; ", validarTurno.Errors.Select(e => e));
                return Result<TurnoReadDTO>.Fail(errors);
            }

            //Mapeo de turno
            var turnoUpdate = _mapperTurno.ToEntity(turno);
            //Actualizar Turno
            await _unitOfWork.TurnoRepository.Update(id, turnoUpdate);
            await _unitOfWork.SaveChangeAsync();

            //Agregar nuevo HistorialTurno de turno.
            //Registro que se conserva para el nuevo registor: Turno.
            //Registros Anteriores y Registros Actuales: EstadoTurno, FechaHora
            //         
            var fechaHoraAnterior = new DateTimeOffset(turnoAnterior.FechaTurno, turnoAnterior.HoraTurno, new TimeSpan(-3));
            var fechaHoraActual = new DateTimeOffset(turnoUpdate.FechaTurno, turnoUpdate.HoraTurno, new TimeSpan(-3));
            var estadoTurnoAnterior = turnoAnterior.EstadoTurnoId;
            var estadoTurnoActual = turnoUpdate.EstadoTurnoId;

            var historialTurno = new HistorialTurno
            {
                TurnoId = turnoUpdate.TurnoId,
                FechaHoraAnterior = fechaHoraAnterior,
                FechaHoraActual = fechaHoraActual,
                EstadoTurnoAnterior = estadoTurnoAnterior,
                EstadoTurnoActual = estadoTurnoActual
            };

            //Agregar nuevo registro de historial turno.
            await _unitOfWork.HistorialTurnoRepository.Add(historialTurno);
            await _unitOfWork.SaveChangeAsync();

            //Actualizacion de Servicio o servicios | Agregar, Quitar, Cambiar(Quitar,Agregar)
            var turnoServicioId = await _unitOfWork.TurnoServicioRepository.GetById(turno.);


            await _unitOfWork.CommitAsync();

            var turnoReadDTO = _mapperTurno.ToReadDTO(turnoUpdate);
            return Result<TurnoReadDTO>.Succes(turnoReadDTO);
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
