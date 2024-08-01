using Microsoft.Extensions.Options;
using PhoenixTask.Application.Abstractions.Emails;
using PhoenixTask.Contracts.Emails;
using PhoenixTask.Infrastructure.Emails.Settings;
using System.Net;
using System.Net.Mail;

namespace PhoenixTask.Infrastructure.Emails;
internal sealed class EmailService(IOptions<MailSettings> maiLSettingsOptions) : IEmailService
{
    private readonly MailSettings _mailSettings= maiLSettingsOptions.Value;

    public Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MailMessage
        {
            From = new MailAddress(_mailSettings.SenderDisplayName, _mailSettings.SenderEmail),
            Subject = mailRequest.Subject,
            Body = mailRequest.Body
        };
        email.To.Add(new MailAddress(mailRequest.EmailTo));

        using var smtpClient = new SmtpClient();
        smtpClient.EnableSsl = true;
        smtpClient.Host = _mailSettings.SmtpServer;
        smtpClient.Port = _mailSettings.SmtpPort;

        var credentials = new NetworkCredential();
        credentials.UserName = _mailSettings.SenderEmail;
        credentials.Password = _mailSettings.SmtpPassword;
        smtpClient.Credentials = credentials;

        smtpClient.Send(email) ;
        return Task.CompletedTask;
    }
}
