using BLL.Result;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITurnoService : IGenericService<Turno>
    {
        Task<Result<IEnumerable<Turno?>>> GetPaged(int pageNumber, int pageSize);
    }
}
