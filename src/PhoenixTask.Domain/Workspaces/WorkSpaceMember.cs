using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces.DomainEvents;

namespace PhoenixTask.Domain.Workspaces;

public sealed class WorkSpaceMember : Member
{
    public WorkSpaceMember(WorkSpace workSpace, User user, ICollection<Role> roles)
        : base(user, roles)
    {

        WorkSpace = workSpace;
        WorkSpaceId = workSpace.Id;
    }
    /// <summary>
    /// efcore
    /// </summary>
#pragma warning disable
    public WorkSpaceMember()
    {
        
    }
    public Guid WorkSpaceId { get; private set; }
    public WorkSpace WorkSpace { get; private set; }
    public static WorkSpaceMember Create(WorkSpace workSpace, User user, params Role[] roles)
    {
        var member = new WorkSpaceMember(workSpace, user, roles);

        member.AddDomainEvent(new WorkSpaceMemberCreatedDomainEvent(member));

        return member;
    }
}
