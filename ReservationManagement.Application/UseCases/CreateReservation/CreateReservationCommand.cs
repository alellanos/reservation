using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Domain.Enums;

namespace ReservationManagement.Application.UseCases.CreateReservation
{
    public class CreateReservationCommand
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime DateTime { get; set; }
        public int PeopleQuantity { get; set; }
        public ReservationType Type { get; set; }

        public string? VipCode { get; set; }
        public int? PreferredTable { get; set; }

        public int? BirthdayAge { get; set; }
        public bool? RequiresCake { get; set; }
    }
}
