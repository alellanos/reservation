
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Infrastructure.Repositories;
using UserSystem.Infrastructure;

namespace ReservationManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReservationDbContext _context;

        private IUserRepository _userRepository;
        private IReservationRepository _reservationRepository;

        public UnitOfWork(ReservationDbContext context)
        {
            _context = context;
        }
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_context);
        public IReservationRepository ReservationRepository => _reservationRepository = _reservationRepository ?? new ReservationRepository(_context);
        public async Task<bool> CommitAsync()
        {
            await _context.SaveChangesAsync();

            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task MigrateDb()
        {
            return _context.MigrateDb();
        }
    }
}
