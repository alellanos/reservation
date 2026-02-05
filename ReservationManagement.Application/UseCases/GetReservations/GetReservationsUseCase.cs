using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Dtos;
using ReservationManagement.Application.Interfaces.Repositories;

namespace ReservationManagement.Application.UseCases.GetReservations
{
    public class GetReservationsUseCase : IGetReservationsUseCase
    {
        private readonly IReservationRepository _repository;

        public GetReservationsUseCase(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReservationSummaryDto>> ExecuteAsync(
            ReservationFilterDto filter)
        {
            var reservations = await _repository.GetByFilterAsync(filter);

            return reservations.Select(r => new ReservationSummaryDto
            {
                Id = r.Id,
                Name = r.Name,
                Email = r.Email.Value,
                Type = r.Type.ToString(),
                DateTime = r.DateTime.Value,
                People = r.PeopleQuantity.Value,
                Status = r.Status.ToString()
            });
        }
    }
}
