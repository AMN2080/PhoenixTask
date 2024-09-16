using PhoenixTask.Domain.Abstractions.Primitives;

namespace PhoenixTask.Domain.Authorities;

public class RolePermission
{
    public int RoleValue { get; set; }
    public int PermissionId { get; set; }
}
