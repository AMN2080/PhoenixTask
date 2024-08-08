using PhoenixTask.Application.Abstractions.Notifications;
using PhoenixTask.Application.Users.CreateUser;
using PhoenixTask.BackgroundTask.Abstractions.Messaging;
using PhoenixTask.Contracts.Emails;
using PhoenixTask.Domain.Abstractions.Exceptions;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.BackgroundTask.IntegrationEvents.Users.UserCreated;

internal sealed class SendWelcomeEmailOnUserCreatedIntegrationEventHandler(IUserRepository userRepository, IEmailNotificationService emailNotificationService) : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailNotificationService _emailNotificationService = emailNotificationService;

    public async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(notification.UserId);

        if (maybeUser.HasNoValue)
        {
            throw new DomainException(DomainErrors.User.NotFound);
        }

        User user = maybeUser.Value;

        var welcomeEmail = new WelcomeEmail(user.Email.Value, user.UserName.Value);

        await _emailNotificationService.SendWelcomeEmail(welcomeEmail);
    }
}
