using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Projects.DomainEvents;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Domain.Projects;

public sealed class ProjectMember : Member
{
    public ProjectMember(Project project, User user, ICollection<Role> roles)
        : base(user, roles)
    {

        Project = project;
        ProjectId = project.Id;
    }
    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; }

    public static ProjectMember Create(Project project, User user, params Role[] roles)
    {
        var member = new ProjectMember(project, user, roles);

        member.AddDomainEvent(new ProjectMemberCreatedDomainEvent(member));

        return member;
    }
}
