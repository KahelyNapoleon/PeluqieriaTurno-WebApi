using DAL.Data;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios
{
    public class TurnoRepository : GenericRepository<Turno>, ITurnoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TurnoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Turno?>> GetPaged(int pageNumber, int pageSize = 10)
        {

            var query = _dbContext.Turnos
                .AsNoTracking()
                .OrderBy(t => t.TurnoId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new Turno
                {
                    TurnoId = t.TurnoId,
                    Detalle = t.Detalle,
                    FechaTurno = t.FechaTurno,
                    HoraTurno = t.HoraTurno,
                    Cliente = t.Cliente,
                    EstadoTurno = t.EstadoTurno
                });
                
           
                return await query.ToListAsync();
        }

        public override async Task<Turno?> GetById(int id)
        {
            var query = _dbContext.Turnos
                 .AsNoTracking()
                 .Include(t => t.Cliente)
                 .Include(t => t.EstadoTurno);
                 


            return await query.FirstOrDefaultAsync(t => t.TurnoId == id);
        }

    }
}
