using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Domain.Tasks.DomainEvents;

public sealed class TaskCreatedDomainEvent : IDomainEvent
{
    internal TaskCreatedDomainEvent(Task task) => Task = task;
    public Task Task { get; }
}
