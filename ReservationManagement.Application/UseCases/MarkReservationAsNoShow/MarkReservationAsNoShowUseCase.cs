using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Application.Interfaces;

namespace ReservationManagement.Application.UseCases.MarkReservationAsNoShow
{
    public class MarkReservationAsNoShowUseCase : IMarkReservationAsNoShowUseCase
    {
        private readonly IReservationRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkReservationAsNoShowUseCase(
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

            reservation.MarkAsNoShow();

            await _unitOfWork.CommitAsync();
        }
    }
}
