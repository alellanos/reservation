using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Domain.Enums;

namespace ReservationManagement.Application.Dtos
{
    public class ReservationFilterDto
    {
        public DateTime? Date { get; set; }
        public ReservationType? Type { get; set; }
        public ReservationStatus? Status { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
