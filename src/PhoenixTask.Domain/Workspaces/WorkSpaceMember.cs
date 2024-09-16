using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces.DomainEvents;

namespace PhoenixTask.Domain.Workspaces;

public sealed class WorkSpaceMember : Member
{
    private WorkSpaceMember(WorkSpace workSpace, User user, Role role)
        : base(user, role,MemberType.WorkSpaceMember)
    {
        Ensure.NotNull(workSpace, "The workspace is requierd.", nameof(workSpace));

        WorkSpaceId = workSpace.Id;
    }
    /// <summary>
    /// efcore
    /// </summary>
#pragma warning disable
    private WorkSpaceMember() { }
    public Guid WorkSpaceId { get; private set; }
    public static WorkSpaceMember Create(WorkSpace workSpace, User user, Role role)
    {
        var member = new WorkSpaceMember(workSpace, user, role);

        member.AddDomainEvent(new WorkSpaceMemberCreatedDomainEvent(member));

        return member;
    } 
}
