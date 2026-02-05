using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces;
using ReservationManagement.Application.Interfaces.Repositories;

namespace ReservationManagement.Application.UseCases.ConfirmReservation
{
    public class ConfirmReservationUseCase : IConfirmReservationUseCase
    {
        private readonly IReservationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmReservationUseCase(
            IReservationRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid reservationId)
        {
            var reservation = await _repository.GetAsync(reservationId)
                ?? throw new ApplicationException("Reserva no encontrada");

            reservation.Confirm();

            await _unitOfWork.CommitAsync();
        }
    }
}
