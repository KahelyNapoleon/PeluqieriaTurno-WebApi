using BLL.Validations;
using DomainLayer.Models;
using FluentValidation;

namespace PeluqueriaTurnoWebApi.ContainerGroupRegistration
{
    public static class CollectionValidation
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Cliente>, ClienteValidator>();
            services.AddScoped<IValidator<EstadoTurno>, EstadoTurnoValidator>();
            services.AddScoped<IValidator<HistorialTurno>, HistorialTurnoValidator>();
            services.AddScoped<IValidator<MetodoPago>, MetodoPagoValidator>();
            services.AddScoped<IValidator<Pago>, PagoValidator>();
            services.AddScoped<IValidator<Servicio>, ServicioValidator>();
            services.AddScoped<IValidator<TipoServicio>, TipoServicioValidator>();
            services.AddScoped<IValidator<TurnoServicio>, TurnoServicioValidator>();
            services.AddScoped<IValidator<Turno>, TurnoValidator>();

            return services;
        }
    }
}
