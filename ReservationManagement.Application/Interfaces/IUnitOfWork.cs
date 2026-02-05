using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces.Repositories;

namespace ReservationManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IReservationRepository ReservationRepository { get; }

        Task<bool> CommitAsync();
        Task MigrateDb();
    }
}
