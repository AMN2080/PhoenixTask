using PhoenixTask.Application.Abstractions.Authentication;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Contracts.Authentication;
using PhoenixTask.Domain.Abstractions.Maybe;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Authentication.Login;

internal sealed class LoginWithUserNameCommandHandler
    (IUserRepository userRepository,
    IPasswordHashChecker passwordHashChecker,
    IJwtProvider jwtProvider)
    : ICommandHandler<LoginWithUserNameCommand, Result<TokenResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHashChecker _passwordHashChecker = passwordHashChecker;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    public async Task<Result<TokenResponse>> Handle(LoginWithUserNameCommand request, CancellationToken cancellationToken)
    {
        var userNameResult = UserName.Create(request.Username);

        if (userNameResult.IsFailure)
        {
            return Result.Failure<TokenResponse>(DomainErrors.User.UsernameNotFound);
        }

        Maybe<User> user = await _userRepository.GetByUsernameAsync(userNameResult.Value);

        if (user.HasNoValue)
        {
            return Result.Failure<TokenResponse>(DomainErrors.User.UserNotFound);
        }

        if (!user.Value.VerifyPasswordHash(request.Password, _passwordHashChecker))
        {
            return Result.Failure<TokenResponse>(DomainErrors.User.BadPassword);
        }

        string token = _jwtProvider.Create(user.Value);
        return Result.Success(new TokenResponse(token));
    }
}
