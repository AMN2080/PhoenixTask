using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

internal sealed class ResetPasswordCommandHandler(IUserRepository userRepository) : ICommandHandler<ResetPasswordCommand, Result>
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var passwordResult = Password.Create(request.Password);

        var result = Result.FirstFailureOrSuccess(emailResult, passwordResult);
        if (result.IsFailure)
        {
            return result;
        }

        var mayUser = await _userRepository.GetByEmailAsync(emailResult.Value);
        if (mayUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var user = mayUser.Value;

        return user.ResetPassword(request.Token, passwordResult.Value);
    }
}
