using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Abstractions.Notifications;
using PhoenixTask.Contracts.Emails;
using PhoenixTask.Domain.Users.DomainEvents;

namespace PhoenixTask.Application.Users.CreateUser;

public sealed class SendWelcomeOnRegisterEvent(IEmailNotificationService emailNotificationService) : IDomainEventHandler<UserCreatedDomainEvent>
{
    private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;
    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        //var welcomeEmail = new WelcomeEmail(notification.User.Email.Value, notification.User.UserName.Value);
        //await _emailNotificationService.SendWelcomeEmail(welcomeEmail);
        await Task.CompletedTask;
    }
}
