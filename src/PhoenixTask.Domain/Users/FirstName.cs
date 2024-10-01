using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;

namespace PhoenixTask.Domain.Users;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 35;
    private FirstName(string value) => Value = value;
    public string Value { get; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    public static implicit operator string(FirstName firstName) => firstName.Value;
    public static Result<FirstName> Create(string name) =>
        Result.Create(name, DomainErrors.Name.NullOrEmpty)
        .Ensure(n => !string.IsNullOrWhiteSpace(n), DomainErrors.Name.NullOrEmpty)
            .Ensure(n => n.Length <= MaxLength, DomainErrors.Name.LongerThanAllowed)
            .Map(n => new FirstName(n));
}