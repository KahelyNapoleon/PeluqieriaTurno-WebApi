using DAL.Repositorios;
using DAL.Repositorios.Interfaces;

namespace PeluqueriaTurnoWebApi.Configuration.ContainerGroupRegistration
{
    public static class CollectionDALRepositories
    {
        public static IServiceCollection AddRepositoriesOfDAL(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEstadoTurnoRepository, EstadoTurnoRepository>();
            services.AddScoped<IHistorialTurnoRepository, HistorialTurnoRepository>();
            services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();
            services.AddScoped<IServicioRepository, ServicioRepository>();
            services.AddScoped<ITipoServicioRepository, TipoServicioRepository>();
            services.AddScoped<ITurnoRepository, TurnoRepository>();
            services.AddScoped<ITurnoServicioRepository, TurnoServicioRepository>();

            return services;
        }
    }
}
