using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(Create(Role.Default, Infrastructure.Permission.CreateWorkSpace));
    }

    static RolePermission Create(
        Role role,
        Infrastructure.Permission permission)
        => new RolePermission
        {
            RoleId = role.Value,
            PermissionId = (int)permission
        };
}