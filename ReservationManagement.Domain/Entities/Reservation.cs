using ReservationManagement.Domain.Enums;
using ReservationManagement.Domain.Exceptions;
using ReservationManagement.Domain.ValueObjects;

namespace ReservationManagement.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public ReservationDateTime DateTime { get; private set; }
    public PeopleQuantity PeopleQuantity { get; private set; }
    public ReservationType Type { get; private set; }
    public ReservationStatus Status { get; private set; }

    public VipInfo? VipInfo { get; private set; }
    public BirthdayInfo? BirthdayInfo { get; private set; }

    protected Reservation() { }

    private Reservation(
        string name,
        Email email,
        ReservationDateTime dateTime,
        PeopleQuantity peopleQuantity,
        ReservationType type)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        DateTime = dateTime;
        PeopleQuantity = peopleQuantity;
        Type = type;
        Status = ReservationStatus.Pending;
    }

    public static Reservation CreateStandard(
        string name,
        Email email,
        DateTime dateTime,
        int people)
    {
        return new Reservation(
            name,
            email,
            new ReservationDateTime(dateTime),
            new PeopleQuantity(people),
            ReservationType.Standard
        );
    }

    public static Reservation CreateVip(
        string name,
        Email email,
        DateTime dateTime,
        int people,
        VipInfo vipInfo)
    {
        var reservation = new Reservation(
            name,
            email,
            new ReservationDateTime(dateTime),
            new PeopleQuantity(people),
            ReservationType.Vip
        );

        reservation.VipInfo = vipInfo;
        reservation.Status = ReservationStatus.Confirmed;

        return reservation;
    }

    public static Reservation CreateBirthday(
        string name,
        Email email,
        DateTime dateTime,
        int people,
        BirthdayInfo birthdayInfo)
    {
        var reservation = new Reservation(
            name,
            email,
            new ReservationDateTime(dateTime),
            new PeopleQuantity(people),
            ReservationType.Birthday
        );

        reservation.BirthdayInfo = birthdayInfo;
        return reservation;
    }

    public void Confirm()
    {
        if (Status != ReservationStatus.Pending)
            throw new DomainException("Solo se pueden confirmar reservas pendientes.");

        Status = ReservationStatus.Confirmed;
    }

    public void Cancel(string reason)
    {
        if (Status is ReservationStatus.Cancelled or ReservationStatus.NoShow)
            throw new DomainException("No se puede cancelar la reserva.");

        Status = ReservationStatus.Cancelled;
    }

    public void MarkAsNoShow()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new DomainException("Solo una reserva confirmada puede marcarse como no asistida.");

        Status = ReservationStatus.NoShow;
    }

    public void Complete()
    {
        if (Status != ReservationStatus.Confirmed)
            throw new DomainException("Solo se pueden completar reservas confirmadas");

        Status = ReservationStatus.Completed;
    }
}
