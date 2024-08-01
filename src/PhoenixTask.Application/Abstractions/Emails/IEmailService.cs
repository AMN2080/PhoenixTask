using PhoenixTask.Contracts.Emails;

namespace PhoenixTask.Application.Abstractions.Emails;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
