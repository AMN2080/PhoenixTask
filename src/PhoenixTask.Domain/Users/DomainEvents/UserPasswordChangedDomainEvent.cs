using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Domain.Users.DomainEvents;

public sealed class UserPasswordChangedDomainEvent : IDomainEvent
{
    internal UserPasswordChangedDomainEvent(User user) => User = user;
    public User User { get; }
}
