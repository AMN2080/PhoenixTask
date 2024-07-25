using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.ChangePassword;

public sealed record ChangePasswordCommand(string Token, string NewPassword) : ICommand<Result>;
