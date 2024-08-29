using PhoenixTask.Domain.Abstractions;
using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Domain.Projects;

public class Project : Entity, IAuditableEntity, ISoftDeletableEntity
{
    private Project(WorkSpace workSpace, Name name)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(name, "the name is requierd.", nameof(name));
        Ensure.NotNull(workSpace, "the workspace is requierd.", nameof(workSpace));

        Name = name;
        WorkSpaceId = workSpace.Id;
    }
#pragma warning disable 
    /// <summary>
    /// Efcore Constructor
    /// </summary>
    public Project() { }
#pragma warning enable
    public Guid WorkSpaceId { get; private set; }
    public Name Name { get; private set; }
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }
    public static Project Create(WorkSpace workSpace, Name name) => new Project(workSpace, name);
    public void UpdateName(Name name)
    {
        Ensure.NotEmpty(name, "the name is requierd.", nameof(name));

        Name = name;
    }
}