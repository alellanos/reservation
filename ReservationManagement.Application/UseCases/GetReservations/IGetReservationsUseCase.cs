using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Dtos;

namespace ReservationManagement.Application.UseCases.GetReservations
{
    public interface IGetReservationsUseCase
    {
        Task<IEnumerable<ReservationSummaryDto>> ExecuteAsync(
            ReservationFilterDto filter
        );
    }
}
