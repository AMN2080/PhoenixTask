using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

public sealed record ForgetPasswordCommand(string Email) : ICommand<Result>;
