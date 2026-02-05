using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Domain.Entities;
using UserSystem.Infrastructure;

namespace ReservationManagement.Application.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    { }
}
