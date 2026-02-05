using ReservationManagement.Domain.Enums;

namespace ReservationManagement.Api.Requests
{
    public class CreateReservationRequest
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public int PeopleQuantity { get; set; }
        public ReservationType Type { get; set; }

        public string? VipCode { get; set; }
        public int? PreferredTable { get; set; }
        public int? BirthdayAge { get; set; }
        public bool? RequiresCake { get; set; }
    }
}
