using DAL.Data;
using DAL.Repositorios;
using DAL.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.IUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IDbContextTransaction _transaction = null!;
        private readonly ApplicationDbContext _dbContext;
        //private bool disposedValue;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ITurnoRepository _turnoRepository;
        public ITurnoRepository TurnoRepository => _turnoRepository ??= new TurnoRepository(_dbContext);
        
        private IHistorialTurnoRepository _historialTurnoRepository;
        public IHistorialTurnoRepository HistorialTurnoRepository => _historialTurnoRepository ??= new HistorialTurnoRepository(_dbContext);

        private ITurnoServicioRepository _turnoServicioRepository;
        public ITurnoServicioRepository TurnoServicioRepository => _turnoServicioRepository ??= new TurnoServicioRepository(_dbContext);

        private IServicioRepository _servicioRepository;
        public IServicioRepository ServicioRepository => _servicioRepository ?? new ServicioRepository(_dbContext);


        public async Task BeginTransactionAsync()
        {
            var beginTransaction = await _dbContext.Database.BeginTransactionAsync();
            if (_transaction != null)
            {
                return;
            }
            _transaction = beginTransaction;
        }

        public async Task CommitAsync()
        {
            if(_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction.Dispose();
                _transaction = null!;
            }
        }

        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
                _transaction = null!;
            }
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.DisposeAsync();
        }

    
    }
}
