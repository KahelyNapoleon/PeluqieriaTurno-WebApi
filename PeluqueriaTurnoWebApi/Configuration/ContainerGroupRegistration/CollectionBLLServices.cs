using BLL.Services;
using BLL.Services.Interfaces;
using DAL.UnitOfWork.IUnitOfWork;

namespace PeluqueriaTurnoWebApi.Configuration.ContainerGroupRegistration
{
    public static class CollectionBLLServices
    {
        public static IServiceCollection AddServicesOfBLL(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IEstadoTurnoService, EstadoTurnoService>();
            services.AddScoped<IHistorialTurnoService, HistorialTurnoService>();
            services.AddScoped<IMetodoPagoService, MetodoPagoService>();
            services.AddScoped<IPagoService, PagoService>();
            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<ITipoServicioService, TipoServicioService>();
            services.AddScoped<ITurnoService, TurnoService>();
            services.AddScoped<ITurnoServicioService, TurnoServicioService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
