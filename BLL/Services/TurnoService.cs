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
using BLL.Services.EstadoTurnoEnum;

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

        //OBTENER POR ID
        //Si lo que se obtiene de GetById es un turno con servicios incluidos y estadoTurnos, entonces falta especificar esos objetos en la clase TurnoReadDTO
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




        //CREAR TURNO PARA EL CLIENTE       
        //1-SE VALIDA EL TURNO
        //A-SE MAPEA EL DTOCREATEUPDATE
        //B-SE AGREGA EL REGISTRO DE TURNO A LA TABLA
        //2-
        public async Task<Result<TurnoReadDTO>> Add(TurnoCreateUpdateDTO turno) //Este metodo no esta reespaldado por la interfaz de IGenericService
        {
            if (turno == null) throw new ArgumentNullException("El registro de turno debe completarse.");

            var validarturno = await _validatorTurno.ValidateAsync(turno);
            if (!validarturno.IsValid)
            {
                var errors = string.Join("; ", validarturno.Errors.Select(e => e.ErrorMessage));
                return Result<TurnoReadDTO>.Fail(errors);
            }

            var servicios = new List<Servicio>();
            foreach (var servicioId in turno.ServiciosId)
            {
                var servicio = await _unitOfWork.ServicioRepository.GetById(servicioId);
                if (servicio == null)
                {
                    return Result<TurnoReadDTO>.Fail($"El servicio con id {servicioId} no existe en los registros");
                }
                servicios.Add(servicio);
            }

            var turnoToEntity = _mapperTurno.ToEntity(turno);

            await _unitOfWork.BeginTransactionAsync();


            try
            { //------------------------------TURNO--------------------------------
               

                
                await _unitOfWork.TurnoRepository.Add(turnoToEntity);

                //---------------------------FIN-TURNO-------------------------------

                //---------------------------HISTORIALTURNO-------------------------------
                //Por ser la primera vez que se crea el turno, el estado anterior seria Turno DISPONIBLE            
                
                //Inicia el historial
                var inicioHistorialTurno = new HistorialTurnoCreateUpdateDTO
                {
                    TurnoId = turnoToEntity.TurnoId,
                    FechaHoraAnterior = null,
                    FechaHoraActual = new DateTimeOffset(turnoToEntity.FechaTurno, turnoToEntity.HoraTurno, TimeSpan.FromHours(-3)),
                    EstadoTurnoAnterior = (int)EstadoTurnoEnum.EstadoTurnoEnum.Disponible,
                    EstadoTurnoActual = (int)EstadoTurnoEnum.EstadoTurnoEnum.Reservado,
                };

                var validarHistorialTurno = await _validatorHistorialTurno.ValidateAsync(inicioHistorialTurno);
                if (!validarHistorialTurno.IsValid)
                {
                    var errors = string.Join("; ", validarHistorialTurno.Errors.Select(e => e.ErrorMessage));
                    return Result<TurnoReadDTO>.Fail(errors);
                }

                var historialTurnoToEntity = _mapperHistorialTurno.ToEntity(inicioHistorialTurno);

                await _unitOfWork.HistorialTurnoRepository.Add(historialTurnoToEntity);

                //---------------------------FIN-HISTORIALTURNO-------------------------------


                //---------------------------TURNOSERVICIO-------------------------------
                //BLOQUE AGREGAR SERVICIOS A TABLA TurnoServicio
                //Crear Registros de los Servicios del turno
                //EN OBSERVACION... ... ...
               
                foreach (var servicio in servicios)
                {
                    var agregarTurnoServicioDTO = new TurnoServicioCreateUpdateDTO
                    {
                        TurnoId = turnoToEntity.TurnoId,//Porque turnoToEntity? porque ya se agrego al esquema de datos 
                        ServicioId = servicio.ServicioId,
                        MontoAplicado = servicio.Precio,
                        TiempoAplicado = servicio.Duracion
                    };

                    //mapear turno servicio a Entity para poder ingresarlo al parametro de repositorio.
                    var servicioEntity = _mapperTurnoServicio.ToEntity(agregarTurnoServicioDTO);

                    await _unitOfWork.TurnoServicioRepository.Add(servicioEntity);

                 
                }
                //---------------------------FIN-TURNOSERVICIO-------------------------------

                await _unitOfWork.SaveChangeAsync();

                await _unitOfWork.CommitAsync();


                var turnoDto = _mapperTurno.ToReadDTO(turnoToEntity);

                return Result<TurnoReadDTO>.Succes(turnoDto);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                return Result<TurnoReadDTO>.Fail($"Error al intentar crear el Turno {ex.Message}");
            }

        }




        public async Task<Result<TurnoReadDTO>> UpdateEstadoTurno(int TurnoId,int estadoTurnoId)
        {
            var turno = await _unitOfWork.TurnoRepository.GetById(TurnoId);
            if (turno == null)
            {
                return Result<TurnoReadDTO>.Fail($"TurnoId {TurnoId} inexistente o incorrecto");
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                turno.EstadoTurnoId = estadoTurnoId;

                await _unitOfWork.SaveChangeAsync();

                var turnoToEntity = _mapperTurno.ToReadDTO(turno);

                return Result<TurnoReadDTO>.Succes(turnoToEntity);

            }catch(Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                return Result<TurnoReadDTO>.Fail("Error al intentar actualizar el estado del turno");
            }

            
        }


        //sI LO QUE QUIERO CONSEGUIR ES CAMBIAR EL ESTADO DEL TURNO, EL NOMBRE Y LA IMPLEMENTACION DEL METODO
        //DEBEN CAMBIAR Y SER DIFERENTE COMO UpdateEstadoTurno y recibir como parametro el estado del Turno actualizado.
        //public async Task<Result<TurnoReadDTO>> Update(int id, TurnoCreateUpdateDTO turnoActualizado)
        //{
        //    if (turnoActualizado == null) throw new ArgumentNullException("Los campos de Turno deben completarse.");


        //    await _unitOfWork.BeginTransactionAsync();

        //    //RECUPERA EL REGISTRO DEL TURNO CON EL ID EN CUESTION
        //    var turnoActual = await _unitOfWork.TurnoRepository.GetById(id);
        //    if (turnoActual == null)
        //    {
        //        return Result<TurnoReadDTO>.Fail($"No existe registro con id {id}");
        //    }
        //    // VALIDACION DE LOS DATOS DEL TurnoDTO
        //    var validarTurnoActualizar = await _validatorTurno.ValidateAsync(turnoActualizado);
        //    if (!validarTurnoActualizar.IsValid)
        //    {
        //        var errors = string.Concat("; ", validarTurnoActualizar.Errors.Select(e => e));
        //        return Result<TurnoReadDTO>.Fail(errors);
        //    }

        //    //VER QUE SE ACTUALIZA DE TURNO PORQUE AHI TAMBIEN SE ENCUENTRAN LOS SERVICIOS [CORREGIR ]
        //    var nuevoRegistrosDeTurno = new TurnoCreateUpdateDTO
        //    {
        //        Detalle = turnoActualizado.Detalle,
        //        ClienteId = turnoActualizado.ClienteId,
        //        EstadoTurnoId = turnoActualizado.EstadoTurnoId,
        //        HoraTurno = turnoActualizado.HoraTurno,
        //        FechaTurno = turnoActualizado.FechaTurno
        //   //[COREGIR] NO CONTIENE LISTA DE SERVICIOS (EN ESTE CASO Turno.cs contiene una lista de turnoServicios.cs del cual debe acceder a cada uno por cada servicio agregado al turno)
        //    };

        //    //var serviciosDelTurno = turnoActualizado.Servicios <---ALGO ASI DEBE SER LA VARIABLE QUE ALMACENE LOS SERVICIOS DEL TURNO A ACTUALIZAR


        //    //MAPEO DEL TURNO A ENTITY
        //    var turnoActualizadoEntity = _mapperTurno.ToEntity(nuevoRegistrosDeTurno);
        //    //ACTUALIZACION DEL TURNO
        //    await _unitOfWork.TurnoRepository.Update(id, turnoActualizadoEntity);
        //    await _unitOfWork.SaveChangeAsync();

        //    //Acciones:
        //    //Agrega nuevo HistorialTurno de turno.
        //    //Registro que se conserva para el nuevo registro: Turno.
        //    //Registros Anteriores y Registros Actuales: EstadoTurno, FechaHora
        //    //         
        //    var fechaHoraActual = new DateTimeOffset(turnoActual.FechaTurno, turnoActual.HoraTurno, new TimeSpan(-3));
        //    var fechaHoraActualizada = new DateTimeOffset(turnoActualizadoEntity.FechaTurno, turnoActualizadoEntity.HoraTurno, new TimeSpan(-3));
        //    var estadoTurnoActual = turnoActual.EstadoTurnoId;
        //    var estadoTurnoActualizado = turnoActualizadoEntity.EstadoTurnoId;

        //    var historialTurno = new HistorialTurno
        //    {
        //        TurnoId = turnoActualizadoEntity.TurnoId,
        //        FechaHoraAnterior = fechaHoraActual,
        //        FechaHoraActual = fechaHoraActualizada,
        //        EstadoTurnoAnterior = estadoTurnoActual,
        //        EstadoTurnoActual = estadoTurnoActual
        //    };

        //    //Agregar nuevo registro de historial turno.
        //    await _unitOfWork.HistorialTurnoRepository.Add(historialTurno);
        //    await _unitOfWork.SaveChangeAsync();

        //    //-------------------------TURNOSERVICIOS ACTUALIZACION--------------------?DONDE SE LLEVA A CABO LA RELACION DE LOS SERVICIOS EN ESTA CAPA DE TURNOUPDATE?

        //    //Actualizacion de Servicio o servicios | Agregar, Quitar, Cambiar(Quitar,Agregar)
        //    //var turnoServicioId = await _unitOfWork.TurnoServicioRepository.GetById(turno.);

        //    var serviciosActuales = turnoActual.TurnoServicios.Select(s => s.ServicioId);
        //    var serviciosActualizado = turnoActualizado.Servicios.Select(s => s.ServicioId);

        //    var serviciosAgregar = serviciosActualizado.Except(serviciosActuales).ToList();
        //    var serviciosEliminar = serviciosActuales.Except(serviciosActualizado).ToList();//Si esto es asi entonces serviciosActualizados contiene los servicio
        //                                                                                    //que ya existian o serviciosActuales

        //    //Recuperar los registros de TurnoServicio, relacionados al TURNO
        //    // con el id correspondiente a los servicios que se quieren eliminar
        //    //ACLARACION: llamada a la variable 'turnoActualizadoEntity' porque es el turno actual en cuestion que se quiere modificar.
        //    //ACLARACION: 'ts' -> turnoServicio.
        //    var eliminarRelaciones = turnoActualizadoEntity.TurnoServicios
        //        .Where(ts => serviciosEliminar.Contains(ts.ServicioId))
        //        .ToList();

        //    //Aquellos ServicioId's que coinciden con los nuevos servicios agregados.  
        //    //ACLARACION: el origen de Datos es 'turnoActualizado' porque de ahi yacen los servicios actualizados del turno
        //    var agregarRelaciones = turnoActualizado.Servicios
        //        .Where(s => serviciosAgregar.Contains(s.ServicioId))
        //        .ToList();


        //    //Una vez con los registros que se quieren eliminar
        //    foreach (var servicio in eliminarRelaciones)
        //    {
        //        await _unitOfWork.TurnoServicioRepository.Remove(servicio);
        //    }

        //    foreach (var servicio in agregarRelaciones)
        //    {

        //        //turnoUpdate.TurnoServicios.Add();
        //        //Aca se puede simplificar y agregar estos objetos a la coleccion de 
        //        //ServicioActualizadoEntity.
        //        //await _unitOfWork.TurnoServicioRepository.Add(
        //         turnoActualizadoEntity.TurnoServicios.Add( 
        //            new TurnoServicio
        //            {
        //                TurnoId = turnoActualizadoEntity.TurnoId,
        //                ServicioId = servicio.ServicioId,
        //                MontoAplicado = servicio.Precio,
        //                TiempoAplicado = servicio.Duracion
        //            }
        //            );
        //    }

        //    await _unitOfWork.SaveChangeAsync();

        //    await _unitOfWork.CommitAsync();

        //    var turnoReadDTO = _mapperTurno.ToReadDTO(turnoActualizadoEntity);
        //    return Result<TurnoReadDTO>.Succes(turnoReadDTO);
        //}





        //Porque se eliminaria un Turno? porque se cancelo? entonces, debe estar ese lugar libre para otros?
        //Turno Se elimina cuando: 1) Cliente no asiste 2) Cliente cancela antes de tiempo 3)Que pasa luego de que el cliente complete su turno -> Se elimina tal registro o se conserva para archivar historial???
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
