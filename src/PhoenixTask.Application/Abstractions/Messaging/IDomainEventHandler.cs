using MediatR;
using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Application.Abstractions.Messaging;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
{
}
