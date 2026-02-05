using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReservationManagement.Domain.Enums;
using ReservationManagement.Domain.Exceptions;

namespace ReservationManagement.Domain.ValueObjects;

public sealed class ReservationDateTime
{
    public DateTime Value { get; private set; }

    public ReservationDateTime() { }

    public ReservationDateTime(DateTime value)
    {
        Value = value;
    }

    public static ReservationDateTime Create(
        DateTime value,
        ReservationType type)
    {
        if (value < DateTime.UtcNow)
            throw new DomainException(
                "No se puede reservar en una fecha pasada.");

        var time = value.TimeOfDay;

        switch (type)
        {
            case ReservationType.Standard
                when time < TimeSpan.FromHours(19)
                  || time > TimeSpan.FromHours(23.5):
                throw new DomainException(
                    "Horario inválido para reserva estándar.");

            case ReservationType.Vip
                when time < TimeSpan.FromHours(12)
                  && time > TimeSpan.FromHours(1):
                throw new DomainException(
                    "Horario inválido para reserva VIP.");

            case ReservationType.Birthday
                when time > TimeSpan.FromHours(23):
                throw new DomainException(
                    "Horario inválido para reserva de cumpleaños.");
        }

        return new ReservationDateTime(value);
    }
}

