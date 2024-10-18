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
    private static readonly Lazy<Regex> KeyFormatRegex =
            new(() => new Regex(KeyRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));

    private Key(string value) => Value = value;
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
