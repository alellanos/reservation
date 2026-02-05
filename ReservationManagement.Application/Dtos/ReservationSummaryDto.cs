using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Application.Dtos
{
    public class ReservationSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public string Email { get; set; }
        public DateTime DateTime { get; set; }
        public int People { get; set; }
        public string Status { get; set; }
    }
}
