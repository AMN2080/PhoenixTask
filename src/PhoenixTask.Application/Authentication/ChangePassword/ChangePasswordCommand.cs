using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.ChangePassword;

public sealed record ChangePasswordCommand(Guid UserId, string Password) : ICommand<Result>;
