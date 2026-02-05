using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Application.UseCases.CancelReservation
{
    public interface ICancelReservationUseCase
    {
        Task ExecuteAsync(Guid reservationId, string reason);
    }
}
