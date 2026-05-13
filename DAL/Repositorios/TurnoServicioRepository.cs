using DAL.Data;
using DAL.Repositorios.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios
{
    public class TurnoServicioRepository : GenericRepository<TurnoServicio>, ITurnoServicioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TurnoServicioRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TurnoServicio>> GetAllServicios()
        {
            var servicios = _dbContext.TurnoServicios
                .AsNoTracking();
            
            return await servicios.ToListAsync();
        
        }

    }
}
