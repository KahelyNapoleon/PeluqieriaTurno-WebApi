using BLL.Mapping;
using BLL.MappingMethods;
using Contracts.DTOs.ClienteDTOs;
using Contracts.DTOs.EstadoTurnoDTOs;
using Contracts.DTOs.HistorialTurnoDTOs;
using Contracts.DTOs.MetodoPagoDTOs;
using Contracts.DTOs.PagoDTOs;
using Contracts.DTOs.ServicioDTOs;
using Contracts.DTOs.TipoServicioDTOs;
using Contracts.DTOs.TurnoDTOs;
using Contracts.DTOs.TurnoServicioDTOs;
using DomainLayer.Models;

namespace PeluqueriaTurnoWebApi.Configuration.ContainerGroupRegistration
{
    public static class CollectionBLLMapping
    {
        public static IServiceCollection AddMappingOfBLL(this IServiceCollection services)
        {
            services.AddScoped<IMappingService<Cliente,ClienteReadDTO,ClienteCreateUpdateDTO>, ClienteMapping>();
            services.AddScoped<IMappingService<EstadoTurno, EstadoTurnoReadDTO,EstadoTurnoCreateUpdateDTO>, EstadoTurnoMapping>();
            services.AddScoped<IMappingService<HistorialTurno,HistorialTurnoReadDTO,HistorialTurnoCreateUpdateDTO>,HistorialTurnoMapping>();
            services.AddScoped<IMappingService<MetodoPago,MetodoPagoReadDTO,MetodoPagoCreateUpdateDTO>,MetodoPagoMapping>();
            services.AddScoped<IMappingService<Pago,PagoReadDTO,PagoCreateUpdateDTO>, PagoMapping>();
            services.AddScoped<IMappingService<Servicio,ServicioReadDTO,ServicioCreateUpdateDTO>, ServicioMapping>();
            services.AddScoped<IMappingService<TipoServicio, TipoServicioReadDTO, TipoServicioCreateUpdateDTO>, TipoServicioMapping>();
            services.AddScoped<IMappingService<Turno,TurnoReadDTO,TurnoCreateUpdateDTO>, TurnoMapping>();
            services.AddScoped<IMappingService<TurnoServicio,TurnoServicioReadDTO,TurnoServicioCreateUpdateDTO>,TurnoServicioMapping>();

            return services;
        }
    }
}
