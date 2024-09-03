using PhoenixTask.Domain.Abstractions.Primitives;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Domain.Authorities;

public class Role : Enumeration<Role>
{
    public Role(int id, string name) : base(id, name) { }
    public Role() { }
    public static readonly Role Default = new(1, nameof(Default));
    public ICollection<Permission> Permissions { get; set; } = [];
    public ICollection<User> Users { get; set; } = [];
}
