using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Users;
using PhoenixTask.Domain.Abstractions.Result;

namespace PhoenixTask.Application.Authentication.Login;

internal sealed class LoginWithUserNameCommandHandler : ICommandHandler<LoginWithUserNameCommand, Result<TokenResponse>>
{
    public Task<Result<TokenResponse>> Handle(LoginWithUserNameCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
