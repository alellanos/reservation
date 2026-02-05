using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Dtos;
using ReservationManagement.Application.Interfaces.Repositories;

namespace ReservationManagement.Application.UseCases.GetReservationDetail
{
    public class GetReservationDetailUseCase : IGetReservationDetailUseCase
    {
        private readonly IReservationRepository _repository;

        public GetReservationDetailUseCase(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReservationDetailDto> ExecuteAsync(Guid reservationId)
        {
            var reservation = await _repository.GetAsync(reservationId)
                ?? throw new ApplicationException("Reserva no encontrada");

            return new ReservationDetailDto
            {
                Id = reservation.Id,
                Name = reservation.Name,
                Email = reservation.Email.Value,
                DateTime = reservation.DateTime.Value,
                People = reservation.PeopleQuantity.Value,
                Type = reservation.Type.ToString(),
                Status = reservation.Status.ToString(),

                VipCode = reservation.VipInfo?.VipCode,
                PreferredTable = reservation.VipInfo?.PreferredTable,

                BirthdayAge = reservation.BirthdayInfo?.BirthdayAge,
                RequiresCake = reservation.BirthdayInfo?.RequiresCake
            };
        }
    }
}
