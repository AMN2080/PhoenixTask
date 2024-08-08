using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Abstractions.Notifications;
using PhoenixTask.Contracts.Emails;
using PhoenixTask.Domain.Users.DomainEvents;

namespace PhoenixTask.Application.Authentication.ChangePassword;

internal sealed class SendPasswordChangedEmailDomainEventHandler(IEmailNotificationService emailNotificationService) : IDomainEventHandler<UserPasswordChangedDomainEvent>
{
    private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;
    public async Task Handle(UserPasswordChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = new PasswordChangedEmail(notification.User.Email.Value, notification.User.UserName.Value);
        await _emailNotificationService.SendPasswordChangedEmail(email);
    }
}
