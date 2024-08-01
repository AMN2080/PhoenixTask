using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Domain.Users.DomainEvents;

public sealed class UserForgetPasswordDomainEvent : IDomainEvent
{
    internal UserForgetPasswordDomainEvent(User user) => User = user;
    public User User { get; }
}
