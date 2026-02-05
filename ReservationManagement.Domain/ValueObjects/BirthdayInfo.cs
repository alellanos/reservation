using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReservationManagement.Domain.Exceptions;

namespace ReservationManagement.Domain.ValueObjects;

public sealed class BirthdayInfo
{
    public int BirthdayAge { get; private set; }
    public bool RequiresCake { get; private set; }

    public BirthdayInfo() { }

    public BirthdayInfo(int birthdayAge, bool requiresCake)
    {
        BirthdayAge = birthdayAge;
        RequiresCake = requiresCake;
    }

    public static BirthdayInfo Create(
        int birthdayAge,
        bool requiresCake,
        DateTime reservationDate)
    {
        if (birthdayAge <= 0)
            throw new DomainException("Edad inválida.");

        if (requiresCake && reservationDate < DateTime.UtcNow.AddHours(48))
            throw new DomainException(
                "Las reservas con torta deben realizarse con 48hs de anticipación."
            );

        return new BirthdayInfo(birthdayAge, requiresCake);
    }
}


