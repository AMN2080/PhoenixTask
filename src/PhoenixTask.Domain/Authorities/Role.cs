using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Authorities;

public class Role : Enumeration<Role>
{
    private Role(int value, string name) : base(value, name) { }
    /// <summary>
    /// efcore
    /// </summary>
    /// <param name="value"></param>
    private Role(int value):base(value,FromValue(value).Value.Name) { }
    public static readonly Role Admin = new(1, nameof(Admin));
    public static readonly Role ProjectManager = new (2, nameof(ProjectManager));
    public static readonly Role TeamMember = new (3, nameof(TeamMember));
    public static readonly Role Viewer = new (4, nameof(Viewer));

    public ICollection<RolePermission> RolePermissions { get; private set; } = [];
}