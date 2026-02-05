using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationManagement.Application.Dtos;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Domain.Entities;
using ReservationManagement.Infrastructure.Persistence;

namespace ReservationManagement.Infrastructure.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationDbContext dbContext)
        : base(dbContext)
        {
        }

        public async Task<IEnumerable<Reservation>> GetByFilterAsync(
    ReservationFilterDto filter)
        {
            IQueryable<Reservation> query = this.DbSet;

            if (filter.Date.HasValue)
                query = query.Where(r => r.DateTime.Value.Date == filter.Date.Value.Date);

            if (filter.Type.HasValue)
                query = query.Where(r => r.Type == filter.Type);

            if (filter.Status.HasValue)
                query = query.Where(r => r.Status == filter.Status);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(r => r.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(r => r.Email.Value.Contains(filter.Email));

            return await query.ToListAsync();
        }
    }
}
