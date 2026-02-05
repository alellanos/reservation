using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReservationManagement.Domain;
using ReservationManagement.Domain.Entities;
using ReservationManagement.Infrastructure.Persistence.configuration;

namespace ReservationManagement.Infrastructure.Persistence
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Reservation> Reservation => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ReservationConfiguration).Assembly
            );
            

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAudit();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAudit()
        {
            var hour = DateTime.UtcNow.AddHours(-3);
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is Audit))
            {
                var audit = item.Entity as Audit;
                audit.Created = hour;
                audit.CreateBy = "Generic";
                audit.Modify = hour;
                audit.ModifyBy = "Generic";
            }
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is Audit))
            {
                var audit = item.Entity as Audit;
                audit.Modify = hour;
                audit.ModifyBy = "Generic";
                item.Property(nameof(audit.CreateBy)).IsModified = false;
                item.Property(nameof(audit.Created)).IsModified = false;
            }
        }

        public Task MigrateDb() => Database.MigrateAsync();
    }
}
