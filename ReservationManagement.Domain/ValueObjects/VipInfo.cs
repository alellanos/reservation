using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReservationManagement.Domain.Exceptions;

namespace ReservationManagement.Domain.ValueObjects;

public sealed class VipInfo
{
    public string VipCode { get; private set; }
    public int? PreferredTable { get; private set; }

    public VipInfo() { }

    public VipInfo(string vipCode, int? preferredTable)
    {
        VipCode = vipCode;
        PreferredTable = preferredTable;
    }

    public static VipInfo Create(string vipCode, int? preferredTable)
    {
        if (string.IsNullOrWhiteSpace(vipCode))
            throw new DomainException("El código VIP es obligatorio.");

        if (vipCode.Length < 6)
            throw new DomainException(
                "El código VIP debe tener al menos 6 caracteres.");

        return new VipInfo(vipCode, preferredTable);
    }
}

