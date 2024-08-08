using MediatR;
using PhoenixTask.Application.Abstractions.Messaging;

namespace PhoenixTask.BackgroundTask.Abstractions.Messaging;
public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
        where TIntegrationEvent : IIntegrationEvent
{
}
