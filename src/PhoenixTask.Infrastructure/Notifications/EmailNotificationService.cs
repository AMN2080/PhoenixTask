using PhoenixTask.Application.Abstractions.Emails;
using PhoenixTask.Application.Abstractions.Notifications;
using PhoenixTask.Contracts.Emails;

namespace PhoenixTask.Infrastructure.Notifications;

internal sealed class EmailNotificationService(IEmailService emailService) : IEmailNotificationService
{
    private readonly IEmailService _emailService = emailService;

    public async Task SendForgetPasswordEmail(ForgetPasswordEmail forgetPasswordEmail)
    {
        var mailRequest = new MailRequest(
                        forgetPasswordEmail.EmailTo,
                        "Recover Account",
                        $"Hello {forgetPasswordEmail.Name}," +
                        Environment.NewLine +
                        Environment.NewLine +
                        $"This is your link to recover your account " + forgetPasswordEmail.Token +
                        Environment.NewLine +
                        $"If this wasn't you , do not share this code with others !" +
                        Environment.NewLine +
                        $"This email send to {forgetPasswordEmail.EmailTo}"
                        ) ;

        await _emailService.SendEmailAsync(mailRequest);
    }

    public async Task SendPasswordChangedEmail(PasswordChangedEmail passwordChangedEmail)
    {
        var mailRequest = new MailRequest(
                passwordChangedEmail.EmailTo,
                "Security Guard",
                $"Hello {passwordChangedEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"Your password changed recently , if it wasn't you reply to this email immediately !"+
                Environment.NewLine+
                $"This email send as security alarm for {passwordChangedEmail.EmailTo}"
                );

        await _emailService.SendEmailAsync(mailRequest);
    }

    public async Task SendWelcomeEmail(WelcomeEmail welcomeEmail)
    {
        var mailRequest = new MailRequest(
                welcomeEmail.EmailTo,
                "Welcome to Phoenix Task! 🎉",
                $"Welcome to Phoenix Task {welcomeEmail.Name}," +
                Environment.NewLine +
                Environment.NewLine +
                $"You have registered with the email {welcomeEmail.EmailTo}.");

        await _emailService.SendEmailAsync(mailRequest);
    }
}
