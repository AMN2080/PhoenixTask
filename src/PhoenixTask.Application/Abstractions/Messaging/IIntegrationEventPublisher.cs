namespace PhoenixTask.Application.Abstractions.Messaging;

public interface IIntegrationEventPublisher
{
    void Publish(IIntegrationEvent integrationEvent);
}
