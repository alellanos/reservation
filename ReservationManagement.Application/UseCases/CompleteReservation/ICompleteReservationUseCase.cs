using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Application.UseCases.CompleteReservation
{
    public interface ICompleteReservationUseCase
    {
        Task ExecuteAsync(Guid reservationId);
    }
}
