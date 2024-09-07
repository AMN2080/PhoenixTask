using PhoenixTask.Domain.Abstractions.Events;

namespace PhoenixTask.Domain.Projects.DomainEvents;

public sealed class ProjectMemberCreatedDomainEvent : IDomainEvent
{
    internal ProjectMemberCreatedDomainEvent(ProjectMember projectMember) => ProjectMember = projectMember;
    public ProjectMember ProjectMember { get; }
}