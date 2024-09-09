using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Authorities;

public class Role : Enumeration<Role>
{
    public Role(int id, string name) : base(id, name) { }
    public Role() { }
    public static readonly Role Admin = new(1, nameof(Admin));
    public static readonly Role ProjectManager = new (2, nameof(ProjectManager));
    public static readonly Role TeamMember = new (3, nameof(TeamMember));
    public static readonly Role Viewer = new (4, nameof(Viewer));
    public ICollection<Permission> Permissions { get; set; } = [];
}
