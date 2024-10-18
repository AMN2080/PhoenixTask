using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using System.Text.RegularExpressions;

namespace PhoenixTask.Domain.Users;

public sealed class Key : ValueObject
{
    public static Key Default => new("Default");
    public const int MaxLength = 130;
    private const string KeyRegexPattern = @"[^a-zA-Z]+";
    private static readonly Lazy<Regex> KeyFormatRegex =
            new(() => new Regex(KeyRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    private Key(string value) => Value = value;
    public static Result<Key> Create(string key)
        => Result.Create(key, DomainErrors.Key.NullOrEmpty)
        .Ensure(x => !string.IsNullOrWhiteSpace(x), DomainErrors.Key.NullOrEmpty)
        .Ensure(x => x.Length <= MaxLength, DomainErrors.Key.LongerThanAllowed)
        .Ensure(x => KeyFormatRegex.Value.IsMatch(x), DomainErrors.Key.InvalidFormat)
        .Map(x => new Key(x));
    public string Value { get; private set; }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
