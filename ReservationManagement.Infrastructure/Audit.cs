using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Infrastructure
{
    public class Audit
    {
        public DateTime Created { get; set; }
        public DateTime Modify { get; set; }
        public string? CreateBy { get; set; }
        public string? ModifyBy { get; set; }

        public string User { get; set; } = "Generaric";
    }
}
