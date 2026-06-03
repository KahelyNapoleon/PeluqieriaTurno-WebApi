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

            //Por ser la primera vez que se crea el turno, el estado anterior corresponde al turno
            // disponible.
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
                var agregarServicioDTO = new TurnoServicioCreateUpdateDTO
                {
                    TurnoId = turnoToEntity.TurnoId,
                    ServicioId = servicio.ServicioId,
                    MontoAplicado = servicio.Precio,
                    TiempoAplicado = servicio.Duracion
                };

                //mapear turno servicio a Entity para poder ingresarlo al parametro de repositorio.
                var agregarServicioEntity = _mapperTurnoServicio.ToEntity(agregarServicioDTO);

                await _unitOfWork.TurnoServicioRepository.Add(agregarServicioEntity);

                await _unitOfWork.SaveChangeAsync();
            }


            await _unitOfWork.CommitAsync();


            var turnoDto = _mapperTurno.ToReadDTO(turnoToEntity);

            return Result<TurnoReadDTO>.Succes(turnoDto);

        }





        //sI LO QUE QUIERO CONSEGUIR ES CAMBIAR EL ESTADO DEL TURNO, EL NOMBRE Y LA IMPLEMENTACION DEL METODO
        //DEBEN CAMBIAR Y SER DIFERENTE COMO UpdateEstadoTurno y recibir como parametro el estado del Turno actualizado.
        public async Task<Result<TurnoReadDTO>> Update(int id, TurnoCreateUpdateDTO turnoActualizado)
        {
            if (turnoActualizado == null) throw new ArgumentNullException("Los campos de Turno deben completarse.");


            await _unitOfWork.BeginTransactionAsync();

            //Recuperar los datos del turno antes de actualizar.
            var turnoActual = await _unitOfWork.TurnoRepository.GetById(id);
            if (turnoActual == null)
            {
                return Result<TurnoReadDTO>.Fail($"No existe registro con id {id}");
            }

           

            var validarTurnoActualizar = await _validatorTurno.ValidateAsync(turnoActualizado);
            if (!validarTurnoActualizar.IsValid)
            {
                var errors = string.Concat("; ", validarTurnoActualizar.Errors.Select(e => e));
                return Result<TurnoReadDTO>.Fail(errors);
            }

            //VER QUE SE ACTUALIZA DE TURNO PORQUE AHI TAMBIEN SE ENCUENTRAN LOS SERVICIOS 
            var nuevoRegistrosDeTurno = new TurnoCreateUpdateDTO
            {
                Detalle = turnoActualizado.Detalle,
                ClienteId = turnoActualizado.ClienteId,
                EstadoTurnoId = turnoActualizado.EstadoTurnoId,
                HoraTurno = turnoActualizado.HoraTurno,
                FechaTurno = turnoActualizado.FechaTurno
            };


            //Mapeo de turno
            var turnoActualizadoEntity = _mapperTurno.ToEntity(nuevoRegistrosDeTurno);
            //Actualizar Turno
            await _unitOfWork.TurnoRepository.Update(id, turnoActualizadoEntity);
            await _unitOfWork.SaveChangeAsync();

            //Acciones:
            //Agrega nuevo HistorialTurno de turno.
            //Registro que se conserva para el nuevo registor: Turno.
            //Registros Anteriores y Registros Actuales: EstadoTurno, FechaHora
            //         
            var fechaHoraActual = new DateTimeOffset(turnoActual.FechaTurno, turnoActual.HoraTurno, new TimeSpan(-3));
            var fechaHoraActualizada = new DateTimeOffset(turnoActualizadoEntity.FechaTurno, turnoActualizadoEntity.HoraTurno, new TimeSpan(-3));
            var estadoTurnoActual = turnoActual.EstadoTurnoId;
            var estadoTurnoActualizado = turnoActualizadoEntity.EstadoTurnoId;

            var historialTurno = new HistorialTurno
            {
                TurnoId = turnoActualizadoEntity.TurnoId,
                FechaHoraAnterior = fechaHoraActual,
                FechaHoraActual = fechaHoraActual,
                EstadoTurnoAnterior = estadoTurnoActual,
                EstadoTurnoActual = estadoTurnoActual
            };

            //Agregar nuevo registro de historial turno.
            await _unitOfWork.HistorialTurnoRepository.Add(historialTurno);
            await _unitOfWork.SaveChangeAsync();



            //Actualizacion de Servicio o servicios | Agregar, Quitar, Cambiar(Quitar,Agregar)
            //var turnoServicioId = await _unitOfWork.TurnoServicioRepository.GetById(turno.);

            var serviciosActuales = turnoActual.TurnoServicios.Select(s => s.ServicioId);
            var serviciosActualizado = turnoActualizado.Servicios.Select(s => s.ServicioId);

            var serviciosAgregar = serviciosActualizado.Except(serviciosActuales).ToList();
            var serviciosEliminar = serviciosActuales.Except(serviciosActualizado).ToList();

            //Busca en los registros de TurnoServicio relacionados al TURNO
            //aquellos registros con el id que corresponde a los servicios que se quieren eliminar
            var eliminarRelaciones = turnoActualizadoEntity.TurnoServicios
                .Where(ts => serviciosEliminar.Contains(ts.ServicioId))
                .ToList();

            //Aquellos ServicioId 's que coinciden con los nuevos servicios agregados.  
            var agregarRelaciones = turnoActualizado.Servicios
                .Where(s => serviciosAgregar.Contains(s.ServicioId))
                .ToList();
                

            //Una vez con los registros que se quieren eliminar
            foreach (var servicio in eliminarRelaciones)
            {
                await _unitOfWork.TurnoServicioRepository.Remove(servicio);
            }

            foreach (var servicio in agregarRelaciones)
            {

                //turnoUpdate.TurnoServicios.Add();
                //Aca se puede simplificar y agregar estos objetos a la coleccion de 
                //ServicioActualizadoEntity.
                //await _unitOfWork.TurnoServicioRepository.Add(
                 turnoActualizadoEntity.TurnoServicios.Add( 
                    new TurnoServicio
                    {
                        TurnoId = turnoActualizadoEntity.TurnoId,
                        ServicioId = servicio.ServicioId,
                        MontoAplicado = servicio.Precio,
                        TiempoAplicado = servicio.Duracion
                    }
                    );
            }

            await _unitOfWork.SaveChangeAsync();

            await _unitOfWork.CommitAsync();

            var turnoReadDTO = _mapperTurno.ToReadDTO(turnoActualizadoEntity);
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



        //Procedimientos

  


    }
}
