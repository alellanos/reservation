using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Application.UseCases.MarkReservationAsNoShow
{
    public interface IMarkReservationAsNoShowUseCase
    {
        Task ExecuteAsync(Guid reservationId);
    }
}
