using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<UpdateUserCommand, Result>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var firstNameResult = !string.IsNullOrEmpty(request.FirstName) ?
            FirstName.Create(request.FirstName) : Result.Success(FirstName.Default);

        var lastNameResult = !string.IsNullOrEmpty(request.LastName) ?
            LastName.Create(request.LastName) : Result.Success(LastName.Default);

        var phoneNumberResult = !string.IsNullOrEmpty(request.PhoneNumber) ?
            PhoneNumber.Create(request.PhoneNumber) : Result.Success(PhoneNumber.Default);

        var result = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, phoneNumberResult);

        if (result.IsFailure)
        {
            return result;
        }

        var maybeUser = await _userRepository.GetByIdAsync(request.UserId);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        var user = maybeUser.Value;

        user.Update(firstNameResult.Value,lastNameResult.Value,phoneNumberResult.Value);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}