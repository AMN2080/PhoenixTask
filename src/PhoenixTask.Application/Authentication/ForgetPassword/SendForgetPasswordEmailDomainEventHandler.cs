using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Abstractions.Notifications;
using PhoenixTask.Contracts.Emails;
using PhoenixTask.Domain.Users.DomainEvents;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

internal sealed class SendForgetPasswordEmailDomainEventHandler(IEmailNotificationService emailNotificationService) : IDomainEventHandler<UserForgetPasswordDomainEvent>
{
    private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;
    public async Task Handle(UserForgetPasswordDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = new ForgetPasswordEmail(notification.User.Email.Value, notification.User.UserName.Value, notification.User.GetToken());
        await _emailNotificationService.SendForgetPasswordEmail(email);
    }
}
