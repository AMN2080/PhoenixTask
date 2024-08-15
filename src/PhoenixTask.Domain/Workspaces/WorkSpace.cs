using PhoenixTask.Domain.Abstractions;
using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Abstractions.Result;
using PhoenixTask.Domain.Errors;

namespace PhoenixTask.Domain.Workspaces;

public sealed class WorkSpace : Entity, IAuditableEntity, ISoftDeletableEntity
{
    public WorkSpace(Name name, string color)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(name, "The name is requierd .", nameof(name));
        Ensure.NotEmpty(color, "The color is requierd .", nameof(color));

        Name = name;
        Color = color;
    }
    public Name Name { get; private set; }
    public string Color { get; private set; }
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }

    public static WorkSpace Create(Name name, string color)
    {
        return new WorkSpace(name, color);
    }
    public Result Update(Name name, string color)
    {
        var nameResult = Name.Create(name);
        var colorResult = Result.Create(color, DomainErrors.Color.NullOrEmpty)
            .Ensure(c => !string.IsNullOrWhiteSpace(c), DomainErrors.Color.NullOrEmpty);

        var result=Result.FirstFailureOrSuccess(nameResult, colorResult);
        if (result.IsFailure)
        {
            return result;
        }

        Name = name;
        Color = color;

        return Result.Success();
    }
}
