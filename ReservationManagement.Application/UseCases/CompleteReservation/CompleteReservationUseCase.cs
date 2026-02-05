using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Application.Interfaces;
using ReservationManagement.Application.UseCases.CancelReservation;

namespace ReservationManagement.Application.UseCases.CompleteReservation
{
    public class CompleteReservationUseCase : ICompleteReservationUseCase
    {
        private readonly IReservationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteReservationUseCase(
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

            reservation.Complete();

            await _unitOfWork.CommitAsync();
        }
    }
}
