using PhoenixTask.Domain.Abstractions;
using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Domain.Projects;

public class Board : Entity , IAuditableEntity , ISoftDeletableEntity
{
    private Board(Project project , Name name,int order,string color)
        :base(Guid.NewGuid())
    {
        Ensure.NotNull(project,"the project is requierd.",nameof(project));
        Ensure.NotEmpty(name,"the name is requierd.",nameof (name));
        Ensure.NotEmpty(color, "the color is requierd.", nameof(color));

        Name = name;
        ProjectId = project.Id;
        Order = order;
        Color = color;
    }
#pragma warning disable
    /// <summary>
    /// Efcore
    /// </summary>
    public Board() { }
#pragma warning enable
    public Guid ProjectId { get; private set; }
    public Name Name { get; private set; }
    public int Order { get; private set; }
    public string Color { get; private set; }
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }

    public static Board Create(Project project, Name name, int order, string color) => new Board(project, name, order, color);
    public void Update(Name name, int order, string color)
    {
        Ensure.NotEmpty(name, "the name is requierd.", nameof(name));
        Ensure.NotEmpty(color, "the color is requierd.", nameof(color));

        Name = name;
        Order = order;
        Color = color;
    }
}
