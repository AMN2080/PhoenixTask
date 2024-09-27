using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Domain.Tasks.DomainEvents;

public sealed class TaskUpdatedDomainEvent : IDomainEvent
{
    internal TaskUpdatedDomainEvent(Task task) => Task = task;
    public Task Task { get; }
}
