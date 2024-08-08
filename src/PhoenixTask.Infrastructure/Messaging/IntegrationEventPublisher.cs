using MediatR;
using PhoenixTask.Application.Abstractions.Messaging;

namespace PhoenixTask.Infrastructure.Messaging;

internal sealed class IntegrationEventPublisher(IMediator mediator) : IIntegrationEventPublisher, IDisposable
{
    private readonly IMediator _mediator = mediator;
    public void Publish(IIntegrationEvent integrationEvent)
    {
        _mediator.Publish(integrationEvent);
    }
    public void Dispose()
    {
        // GC.Collect();
    }
}