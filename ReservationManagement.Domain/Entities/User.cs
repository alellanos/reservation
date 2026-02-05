using ReservationManagement.Domain.Exceptions;
using ReservationManagement.Domain.ValueObjects;

namespace ReservationManagement.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public bool IsActive { get; private set; }

    protected User() { }

    public User(string name, Email email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("El nombre del usuario es obligatorio.");

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}
