using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Authentication.ForgetPassword;

internal sealed class ForgetPasswordCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<ForgetPasswordCommand, Result>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        var mayUser = await _userRepository.GetByEmailAsync(emailResult.Value);

        if (mayUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var user = mayUser.Value;

        user.ForgetPassword();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}