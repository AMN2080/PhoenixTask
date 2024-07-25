using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using System.Text.RegularExpressions;

namespace PhoenixTask.Domain.Users;

public sealed class UserName : ValueObject
{
    public const int MaxLength = 30;
    private const string UserNameRegexPattern
        = @"^(?=[a-zA-Z0-9._]{5,20}$)(?!.*[_.]{2})[^_.].*[^_.]$";
    private static readonly Lazy<Regex> UserNameFormatRegex =
            new(() => new Regex(UserNameRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    private UserName(string value) => Value = value;
    public string Value { get; }

    public static implicit operator string(UserName userName) => userName.Value;
    public static Result<UserName> Create(string userName) =>
        Result.Create(userName, DomainErrors.UserName.NullOrEmpty)
        .Ensure(u => !string.IsNullOrWhiteSpace(u), DomainErrors.UserName.NullOrEmpty)
            .Ensure(u => u.Length <= MaxLength, DomainErrors.UserName.LongerThanAllowed)
            .Ensure(u => UserNameFormatRegex.Value.IsMatch(u), DomainErrors.UserName.InvalidFormat)
            .Map(u => new UserName(u));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
