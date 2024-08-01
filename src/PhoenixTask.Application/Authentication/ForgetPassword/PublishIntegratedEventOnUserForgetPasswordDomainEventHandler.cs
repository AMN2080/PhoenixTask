using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Users.DomainEvents;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

internal class PublishIntegratedEventOnUserForgetPasswordDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher) : IDomainEventHandler<UserForgetPasswordDomainEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher = integrationEventPublisher;
    public async Task Handle(UserForgetPasswordDomainEvent notification, CancellationToken cancellationToken)
    {
        _integrationEventPublisher.Publish(new UserForgetPasswordIntegrationEvent(notification));

        await Task.CompletedTask;
    }
}
