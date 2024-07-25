using PhoenixTask.Domain.Abstractions;
using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using PhoenixTask.Domain.Users.DomainEvents;

namespace PhoenixTask.Domain.Users;

public sealed class User : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{
    private string _passwordHash;
    private User(UserName userName, Email email , string passwordHash)
        :base(Guid.NewGuid())
    {
        Ensure.NotEmpty(userName, nameof(userName),"Username is requierd.");
        Ensure.NotEmpty(email, nameof(email),"Email is requierd.");
        Ensure.NotEmpty(passwordHash, nameof(passwordHash), "Password Hash is requierd.");

        UserName= userName;
        Email = email;
        _passwordHash = passwordHash;
    }
    public UserName UserName { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool Deleted { get; }
    public static User Create(UserName username, Email email, string passwordHash)
    {
        var user = new User(username, email, passwordHash);

        user.AddDomainEvent(new UserCreatedDomainEvent(user));

        return user;
    }

    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
            => !string.IsNullOrWhiteSpace(password) && passwordHashChecker.HashesMatch(_passwordHash, password);

    public Result ChangePassword(string passwordHash)
    {
        if (passwordHash == _passwordHash)
        {
            return Result.Failure(DomainErrors.User.CannotChangePassword);
        }

        _passwordHash = passwordHash;

        AddDomainEvent(new UserPasswordChangedDomainEvent(this));

        return Result.Success();
    }
}