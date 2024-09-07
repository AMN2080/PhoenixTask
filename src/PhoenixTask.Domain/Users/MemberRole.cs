using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Domain.Users;

public class WorkSpaceMemberRole
{
    public int RoleId { get; }
    public WorkSpaceMember WorkSpaceMember { get; }
    public Guid WorkSpaceMemberId { get; }

    //efcore
#pragma warning disable
    public WorkSpaceMemberRole()
    {
        
    }
}
public class ProjectMemberRole
{
    public int RoleId { get; }
    public ProjectMember ProjectMember { get; }
    public Guid ProjectMemberId { get; }
    public ProjectMemberRole()
    {
        
    }
}