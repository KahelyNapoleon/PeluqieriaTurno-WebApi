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
    public class HistorialTurnoService(IHistorialTurnoRepository historialTurnoRepository, IValidator<HistorialTurno> validator) : GenericService<HistorialTurno>(historialTurnoRepository, validator), IHistorialTurnoService
    {
    }
}
