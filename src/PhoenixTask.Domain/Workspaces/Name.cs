using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;

namespace PhoenixTask.Domain.Workspaces;

public sealed class Name : ValueObject
{
    public const int MaxLength = 50;
    private Name(string value) => Value = value;
    public string Value { get; }

    public static implicit operator string(Name name) => name.Value;
    public static Result<Name> Create(string name) =>
        Result.Create(name, DomainErrors.Name.NullOrEmpty)
        .Ensure(n => !string.IsNullOrWhiteSpace(n), DomainErrors.Name.NullOrEmpty)
            .Ensure(n => n.Length <= MaxLength, DomainErrors.Name.LongerThanAllowed)
            .Map(n => new Name(n));
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
