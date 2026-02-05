using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Application.UseCases.ConfirmReservation
{
    public interface IConfirmReservationUseCase
    {
        Task ExecuteAsync(Guid reservationId);
    }
}
