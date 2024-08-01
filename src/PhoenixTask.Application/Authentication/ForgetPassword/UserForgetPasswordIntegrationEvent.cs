using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Users.DomainEvents;
using System.Text.Json.Serialization;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

public sealed class UserForgetPasswordIntegrationEvent : IIntegrationEvent
{
    internal UserForgetPasswordIntegrationEvent(UserForgetPasswordDomainEvent userForgetPasswordDomain)
        =>UserId=userForgetPasswordDomain.User.Id;
    [JsonConstructor]
    private UserForgetPasswordIntegrationEvent(Guid userId) => UserId = userId;
    public Guid UserId { get; }
}