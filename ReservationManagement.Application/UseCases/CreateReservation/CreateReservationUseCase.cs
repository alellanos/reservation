using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationManagement.Application.Interfaces.Repositories;
using ReservationManagement.Application.Interfaces;
using ReservationManagement.Domain.Entities;
using ReservationManagement.Domain.ValueObjects;
using ReservationManagement.Domain.Enums;

namespace ReservationManagement.Application.UseCases.CreateReservation
{
    public class CreateReservationUseCase : ICreateReservationUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationUseCase(
            IUserRepository userRepository,
            IReservationRepository reservationRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateReservationCommand command)
        {
            var user = await _userRepository.GetAsync(command.UserId)
                ?? throw new ApplicationException("Usuario no existe");

            var email = new Email(command.Email);

            Reservation reservation = command.Type switch
            {
                ReservationType.Standard => Reservation.CreateStandard(
                    command.Name,
                    email,
                    command.DateTime,
                    command.PeopleQuantity
                ),

                ReservationType.Vip => Reservation.CreateVip(
                    command.Name,
                    email,
                    command.DateTime,
                    command.PeopleQuantity,
                    new VipInfo(
                        command.VipCode
                            ?? throw new ApplicationException("Código VIP requerido"),
                        command.PreferredTable
                    )
                ),

                ReservationType.Birthday => Reservation.CreateBirthday(
                    command.Name,
                    email,
                    command.DateTime,
                    command.PeopleQuantity,
                    new BirthdayInfo(
                        command.BirthdayAge
                            ?? throw new ApplicationException("Edad requerida"),
                        command.RequiresCake ?? false
                    )
                ),

                _ => throw new ApplicationException("Tipo de reserva inválido")
            };

            await _reservationRepository.Add(reservation);
            await _unitOfWork.CommitAsync();
        }
    }

}
