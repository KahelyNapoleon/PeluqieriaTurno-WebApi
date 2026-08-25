using DAL.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITurnoRepository TurnoRepository { get; }

        IHistorialTurnoRepository HistorialTurnoRepository { get; }

        ITurnoServicioRepository TurnoServicioRepository { get; }

        IServicioRepository ServicioRepository { get; }

        Task BeginTransactionAsync();
        Task SaveChangeAsync();
        Task CommitAsync();
        Task RollBackAsync();

    }
}
