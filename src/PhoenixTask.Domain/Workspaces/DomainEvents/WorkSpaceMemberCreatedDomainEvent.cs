using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Domain.Workspaces.DomainEvents;

public sealed class WorkSpaceMemberCreatedDomainEvent : IDomainEvent
{
    internal WorkSpaceMemberCreatedDomainEvent(WorkSpaceMember workSpaceMember) => WorkSpaceMember = workSpaceMember;
    public WorkSpaceMember WorkSpaceMember { get; }
}