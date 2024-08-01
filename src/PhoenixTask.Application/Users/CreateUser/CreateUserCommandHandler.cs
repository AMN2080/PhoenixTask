using PhoenixTask.Application.Abstractions.Cryptography;
using PhoenixTask.Application.Abstractions.Data;
using PhoenixTask.Application.Abstractions.Messaging;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler
    (IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<CreateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var userNameResult = UserName.Create(request.UserName);
        var passwordResult = Password.Create(request.Password);

        var result = Result.FirstFailureOrSuccess(emailResult, userNameResult, passwordResult);

        if (result.IsFailure)
        {
            return Result.Failure<string>(result.Error);
        }

        if (!await _userRepository.IsEmailUniqueAsync(emailResult.Value))
        {
            return Result.Failure<string>(DomainErrors.User.DuplicateEmail);
        }

        if (!await _userRepository.IsUsernameUniqueAsync(userNameResult.Value))
        {
            return Result.Failure<string>(DomainErrors.User.DuplicateUsername);
        }

        var user = User.Create(userNameResult.Value, emailResult.Value, _passwordHasher.HashPassword(passwordResult.Value));

        _userRepository.Insert(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.ToString();
    }
}
