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
    private string? _authKey;
    private User(UserName userName, Email email, string passwordHash)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(userName, nameof(userName), "Username is requierd.");
        Ensure.NotEmpty(email, nameof(email), "Email is requierd.");
        Ensure.NotEmpty(passwordHash, nameof(passwordHash), "Password Hash is requierd.");

        UserName = userName;
        Email = email;
        _passwordHash = passwordHash;
        IsChangePassword = false;
    }
#pragma warning disable
    /// <summary>
    /// for ef core
    /// </summary>
    public User()
    {

    }
#pragma warning enable
    public UserName UserName { get; private set; }
    public Email Email { get; private set; }
    public bool IsChangePassword { get; private set; }
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

        IsChangePassword = false;
        return Result.Success();
    }

    public void ForgetPassword()
    {
        if (IsChangePassword == false)
        {
            GenerateToken();
        }

        IsChangePassword = true;

        AddDomainEvent(new UserForgetPasswordDomainEvent(this));
    }
    public Result ResetPassword(string token,string password)
    {
        if (IsChangePassword == false)
        {
            return Result.Failure(DomainErrors.User.CannotChangePassword);
        }
        if(!CheckToken(token))
        {
            return Result.Failure(DomainErrors.User.CannotChangePassword);
        }
        return ChangePassword(password);
    }
    public void GenerateToken()
    {
        byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        byte[] key = Guid.NewGuid().ToByteArray();
        string token = Convert.ToBase64String(time.Concat(key).ToArray());

        _authKey = token;
    }
    private bool CheckToken(string token) => _authKey == token;
    public string GetToken() => _authKey;
}