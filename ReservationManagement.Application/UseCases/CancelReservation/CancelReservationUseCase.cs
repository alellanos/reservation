using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Application.Interfaces;

namespace ReservationManagement.Application.UseCases.CancelReservation
{
    public class CancelReservationUseCase : ICancelReservationUseCase
    {
        private readonly IReservationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelReservationUseCase(
            IReservationRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid reservationId, string reason)
        {
            var reservation = await _repository.GetAsync(reservationId)
                ?? throw new ApplicationException("Reserva no encontrada");

            reservation.Cancel(reason);

            await _unitOfWork.CommitAsync();
        }
    }
}
