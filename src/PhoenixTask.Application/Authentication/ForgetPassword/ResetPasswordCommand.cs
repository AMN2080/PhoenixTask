using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

public sealed record ResetPasswordCommand(string Token,string Password):ICommand<Result>;
