using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    internal static RolePermission[] RolePermissions =
        {
        #region AdminRole

            Create(Role.Admin, PermissionType.UpdateWorkSpace),
            Create(Role.Admin, PermissionType.DeleteWorkSpace),
            Create(Role.Admin, PermissionType.ReadWorkSpace),
            Create(Role.Admin, PermissionType.ManageUsers),
            Create(Role.Admin, PermissionType.CreateProject),
            Create(Role.Admin, PermissionType.UpdateProject),
            Create(Role.Admin, PermissionType.DeleteProject),
            Create(Role.Admin, PermissionType.ReadProject),
            Create(Role.Admin, PermissionType.CreateBoard),
            Create(Role.Admin, PermissionType.UpdateBoard),
            Create(Role.Admin, PermissionType.DeleteBoard),
            Create(Role.Admin, PermissionType.ReadBoard),
            Create(Role.Admin, PermissionType.ManageAdmin),

        #endregion

        #region ProjectManagerRole

        Create(Role.ProjectManager, PermissionType.CreateProject),
        Create(Role.ProjectManager, PermissionType.UpdateProject),
        Create(Role.ProjectManager, PermissionType.DeleteProject),
        Create(Role.ProjectManager, PermissionType.ReadProject),
        Create(Role.ProjectManager, PermissionType.ManageUsers),
        Create(Role.ProjectManager, PermissionType.CreateBoard),
        Create(Role.ProjectManager, PermissionType.UpdateBoard),
        Create(Role.ProjectManager, PermissionType.DeleteBoard),
        Create(Role.ProjectManager, PermissionType.ReadBoard),

        #endregion

        #region TeamMemberRole

        Create(Role.TeamMember, PermissionType.ReadWorkSpace),
        Create(Role.TeamMember, PermissionType.ReadProject),
        Create(Role.TeamMember, PermissionType.ReadBoard),

        #endregion

        #region ViewerRole

        Create(Role.Viewer, PermissionType.ReadWorkSpace),
        Create(Role.Viewer, PermissionType.ReadProject),
        Create(Role.Viewer, PermissionType.ReadBoard)

        #endregion
    };
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleValue, x.PermissionId });

        builder.HasData(RolePermissions);
    }

    static RolePermission Create(
        Role role,
        PermissionType permission)
        => new RolePermission
        {
            RoleValue = role.Value,
            PermissionId = (int)permission
        };
}