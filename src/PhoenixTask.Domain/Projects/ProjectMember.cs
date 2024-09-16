using PhoenixTask.Domain.Abstractions.Guards;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Projects.DomainEvents;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Domain.Projects;

public sealed class ProjectMember : Member
{
    public ProjectMember(Project project, User user, Role role)
        : base(user, role, MemberType.ProjectMember)
    {
        Ensure.NotNull(project, "The project is requierd.", nameof(project));

        ProjectId = project.Id;
    }
    /// <summary>
    /// efcore
    /// </summary>
    #pragma warning disable
    private ProjectMember() { }
    public Guid ProjectId { get; private set; }

    public static ProjectMember Create(Project project, User user, Role role)
    {
        var member = new ProjectMember(project, user, role);

        member.AddDomainEvent(new ProjectMemberCreatedDomainEvent(member));

        return member;
    }
}
