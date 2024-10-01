using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;
using System.Text.RegularExpressions;

namespace PhoenixTask.Domain.Users;

public sealed class PhoneNumber : ValueObject
{
    public const int MaxLength = 11;
    private const string PhoneNumberRegexPattern
        = @"^(?:(?:(?:\\+?|00)(98))|(0))?((?:90|91|92|93|99)[0-9]{8})$";
    private static readonly Lazy<Regex>PhoneNumberFormatRegex =
            new(() => new Regex(PhoneNumberRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));
    private PhoneNumber(string value) => Value = value;
    public static Result<PhoneNumber> Create(string phoneNumber) =>
        Result.Create(phoneNumber, DomainErrors.PhoneNumber.NullOrEmpty)
        .Ensure(p => !string.IsNullOrWhiteSpace(p), DomainErrors.PhoneNumber.NullOrEmpty)
            .Ensure(p => p.Length <= MaxLength, DomainErrors.PhoneNumber.LongerThanAllowed)
            .Ensure(p => PhoneNumberFormatRegex.Value.IsMatch(p), DomainErrors.PhoneNumber.InvalidFormat)
            .Map(p => new PhoneNumber(p));
    public string Value { get; }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}