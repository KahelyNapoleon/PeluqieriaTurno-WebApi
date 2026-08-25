using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios.Interfaces
{
    public interface ITurnoRepository : IGenericRepository<Turno>
    {
        Task<IEnumerable<Turno?>> GetPaged(int pageNumber, int pageSize);

        Task<IEnumerable<Turno?>> GetAllTurnoWithServices();
    }
}
