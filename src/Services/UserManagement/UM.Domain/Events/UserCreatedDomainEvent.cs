namespace UM.Domain.Events;

public class UserCreatedDomainEvent : DomainEvent
{
    public User User { get; }

    public UserCreatedDomainEvent(User user)
    {
        User = user;
    }
}