using PhoenixTask.Domain.Abstractions;
using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Tasks.DomainEvents;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Domain.Tasks;

public sealed class Task : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    private Task(Name name,User user,Board board, string description, DateTime deadLine, int priority, int order)
        :base(Guid.NewGuid())
    {
        Ensure.NotEmpty(name, "The name is required.",nameof(name));
        Ensure.NotEmpty(description, "The description is required.",nameof(description));
        Ensure.NotNull(user, "The user is required.", nameof(user));
        Ensure.NotNull(board, "The board is required.", nameof(board));

        Name = name;
        Description = description;
        DeadLine = deadLine;
        Priority = priority;
        Order = order;
        CreatorId = user.Id;
        BoardId = board.Id;
    }
    /// <summary>
    /// efcore
    /// </summary>
    private Task()
    {
        
    }
    public static Task Create(Name name, User user, Board board ,string description, DateTime deadLine, int priority, int order)
    {
        var task =new Task(name, user,board, description, deadLine, priority, order);

        task.AddDomainEvent(new TaskCreatedDomainEvent(task));

        return task;
    }

    void Update(Name name, string description, DateTime deadLine, int priority, int order)
    {
        Ensure.NotEmpty(name, "The name is required.", nameof(name));
        Ensure.NotEmpty(description, "The description is required.", nameof(description));

        Name = name;
        Description = description;
        DeadLine = deadLine;
        Priority = priority;
        Order = order;

        AddDomainEvent(new TaskUpdatedDomainEvent(this));
    }

    public Name Name { get; private set; }
    public string Description { get; private set; }
    public DateTime DeadLine { get; private set; }
    public int Priority { get; private set; }
    public int Order { get; private set; }
    public Guid CreatorId { get;private set; }
    public Guid BoardId { get;private set; }
    public DateTime CreatedOnUtc  { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool Deleted { get; }
}
