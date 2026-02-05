using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using ReservationManagement.Domain.Exceptions;

namespace ReservationManagement.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; private set; }

    public Email() { }

    public Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email vacío");

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new DomainException("Email inválido");

        return new Email(value);
    }

    public override string ToString() => Value;
}

