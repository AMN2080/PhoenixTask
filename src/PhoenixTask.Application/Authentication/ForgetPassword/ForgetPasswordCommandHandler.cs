using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

internal sealed class ForgetPasswordCommandHandler : ICommandHandler<ForgetPasswordCommand, Result>
{
    public Task<Result> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}