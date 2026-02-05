using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReservationManagement.Domain.Enums;
using ReservationManagement.Domain.Exceptions;

namespace ReservationManagement.Domain.ValueObjects;

public sealed class PeopleQuantity
{
    public int Value { get; private set; }

    public PeopleQuantity() { }

    public PeopleQuantity(int value)
    {
        Value = value;
    }

    public static PeopleQuantity Create(int value, ReservationType type)
    {
        switch (type)
        {
            case ReservationType.Standard when value < 1 || value > 4:
                throw new DomainException(
                    "La reserva estándar permite entre 1 y 4 personas.");

            case ReservationType.Birthday when value < 5 || value > 12:
                throw new DomainException(
                    "La reserva de cumpleaños permite entre 5 y 12 personas.");

            case ReservationType.Vip when value < 1:
                throw new DomainException(
                    "La reserva VIP debe tener al menos una persona.");
        }

        return new PeopleQuantity(value);
    }
}
