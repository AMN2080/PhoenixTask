using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

public sealed record ResetPasswordCommand(string Email,string Token,string Password):ICommand<Result>;
