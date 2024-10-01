using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;

namespace PhoenixTask.Domain.Users;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 35;
    private LastName(string value) => Value = value;
    public string Value { get; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    public static implicit operator string(LastName lastName) => lastName.Value;
    public static Result<LastName> Create(string name) =>
        Result.Create(name, DomainErrors.Name.NullOrEmpty)
        .Ensure(n => !string.IsNullOrWhiteSpace(n), DomainErrors.Name.NullOrEmpty)
            .Ensure(n => n.Length <= MaxLength, DomainErrors.Name.LongerThanAllowed)
            .Map(n => new LastName(n));
}