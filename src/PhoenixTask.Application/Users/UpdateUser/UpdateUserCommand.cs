using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string? FirstName, string? LastName, string? PhoneNumber) : ICommand<Result>;
