using PhoenixTask.Contracts.Emails;

namespace PhoenixTask.Application.Abstractions.Notifications;

public interface IEmailNotificationService
{
    Task SendWelcomeEmail(WelcomeEmail welcomeEmail);
    Task SendPasswordChangedEmail(PasswordChangedEmail passwordChangedEmail);
    Task SendForgetPasswordEmail(ForgetPasswordEmail forgetPasswordEmail);
}
