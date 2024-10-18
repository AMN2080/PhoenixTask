using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using System.Text.RegularExpressions;

namespace PhoenixTask.Domain.Users;

public sealed class Key : ValueObject
{
    public static Key Default => new("Default");
    public const int MaxLength = 130;
    private const string KeyRegexPattern
        = @"[A-Za-z]+";
    private static readonly Lazy<Regex> KeyFormatRegex =
            new(() => new Regex(KeyRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    private Key(string value) => Value = value;
    public string Value { get; }

    public static implicit operator string(Key key) => key.Value;
    public static Result<Key> Create(string key) =>
        Result.Create(key, DomainErrors.Key.NullOrEmpty)
        .Ensure(u => !string.IsNullOrWhiteSpace(u), DomainErrors.Key.NullOrEmpty)
            .Ensure(u => u.Length <= MaxLength, DomainErrors.Key.LongerThanAllowed)
            .Ensure(u => KeyFormatRegex.Value.IsMatch(u), DomainErrors.Key.InvalidFormat)
            .Map(u => new Key(u));

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}