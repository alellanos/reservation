using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Domain.Entities;
using ReservationManagement.Infrastructure.Persistence;

namespace ReservationManagement.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ReservationDbContext dbContext)
        : base(dbContext)
        {
        }
    }
}
