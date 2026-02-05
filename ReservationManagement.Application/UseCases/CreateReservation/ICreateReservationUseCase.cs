using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationManagement.Application.UseCases.CreateReservation
{
    public interface ICreateReservationUseCase
    {
        Task ExecuteAsync(CreateReservationCommand command);
    }
}
