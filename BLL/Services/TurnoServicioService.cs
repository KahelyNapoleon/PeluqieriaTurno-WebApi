using BLL.Services.Interfaces;
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
    public class TurnoServicioService(ITurnoServicioRepository turnoServicioRepository, IValidator<TurnoServicio> validator) : GenericService<TurnoServicio>(turnoServicioRepository, validator), ITurnoServicioService
    {
    }
}
