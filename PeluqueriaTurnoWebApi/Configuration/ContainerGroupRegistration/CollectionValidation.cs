using BLL.Validations;
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
using FluentValidation;

namespace PeluqueriaTurnoWebApi.Configuration.ContainerGroupRegistration
{
    public static class CollectionValidation
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ClienteCreateUpdateDTO>, ClienteValidator>();
            services.AddScoped<IValidator<EstadoTurnoCreateUpdateDTO>, EstadoTurnoValidator>();
            services.AddScoped<IValidator<HistorialTurnoCreateUpdateDTO>, HistorialTurnoValidator>();
            services.AddScoped<IValidator<MetodoPagoCreateUpdateDTO>, MetodoPagoValidator>();
            services.AddScoped<IValidator<PagoCreateUpdateDTO>, PagoValidator>();
            services.AddScoped<IValidator<ServicioCreateUpdateDTO>, ServicioValidator>();
            services.AddScoped<IValidator<TipoServicioCreateUpdateDTO>, TipoServicioValidator>();
            services.AddScoped<IValidator<TurnoServicioCreateUpdateDTO>, TurnoServicioValidator>();
            services.AddScoped<IValidator<TurnoCreateUpdateDTO>, TurnoValidator>();

            return services;
        }
    }
}
