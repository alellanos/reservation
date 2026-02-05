using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Domain.Enums
{
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        NoShow,
        Completed
    }
}
