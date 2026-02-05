using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ReservationManagement.Domain.Entities;

namespace ReservationManagement.Infrastructure.Persistence.configuration
{
    public class ReservationConfiguration
        : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(r => r.Type)
                   .IsRequired();

            builder.Property(r => r.Status)
                   .IsRequired();


            builder.OwnsOne(r => r.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("email")
                     .HasMaxLength(255)
                     .IsRequired();
            });

            builder.OwnsOne(r => r.PeopleQuantity, pq =>
            {
                pq.Property(p => p.Value)
                  .HasColumnName("people_quantity")
                  .IsRequired();
            });

            builder.OwnsOne(r => r.DateTime, dt =>
            {
                dt.Property(d => d.Value)
                  .HasColumnName("reservation_datetime")
                  .IsRequired();
            });

            builder.OwnsOne(r => r.VipInfo, vip =>
            {
                vip.Property(v => v.VipCode)
                   .HasColumnName("vip_code")
                   .HasMaxLength(50);

                vip.Property(v => v.PreferredTable)
                   .HasColumnName("preferred_table");
            });

            builder.OwnsOne(r => r.BirthdayInfo, birthday =>
            {
                birthday.Property(b => b.BirthdayAge)
                        .HasColumnName("birthday_age");

                birthday.Property(b => b.RequiresCake)
                        .HasColumnName("requires_cake");
            });


            builder.Navigation(r => r.VipInfo)
                   .IsRequired(false);

            builder.Navigation(r => r.BirthdayInfo)
                   .IsRequired(false);
        }
    }
}
