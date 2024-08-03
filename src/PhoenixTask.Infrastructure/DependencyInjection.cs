using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Common;
using PhoenixTask.Application.Abstractions.Cryptography;
using PhoenixTask.Application.Abstractions.Emails;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Application.Abstractions.Notifications;
using PhoenixTask.Domain.Users;
using PhoenixTask.Infrastructure.Authentication;
using PhoenixTask.Infrastructure.Authentication.Settings;
using PhoenixTask.Infrastructure.Common;
using PhoenixTask.Infrastructure.Cryptography;
using PhoenixTask.Infrastructure.Emails;
using PhoenixTask.Infrastructure.Emails.Settings;
using PhoenixTask.Infrastructure.Messaging;
using PhoenixTask.Infrastructure.Notifications;
using System.Text;

namespace PhoenixTask.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]))
            });

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddTransient<IDateTime, MachineDateTime>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddTransient<IPasswordHashChecker, PasswordHasher>();

        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IEmailNotificationService, EmailNotificationService>();

        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

        return services;
    }
}
