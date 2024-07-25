using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Domain.Users.DomainEvents;

public sealed class UserCreatedDomainEvent : IDomainEvent
{
    internal UserCreatedDomainEvent(User user) => User = user;
    public User User { get; } 
}
