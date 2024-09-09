using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(

        #region AdminRole

            Create(Role.Admin, Infrastructure.Permission.UpdateWorkSpace),
            Create(Role.Admin, Infrastructure.Permission.DeleteWorkSpace),
            Create(Role.Admin, Infrastructure.Permission.ReadWorkSpace),
            Create(Role.Admin, Infrastructure.Permission.ManageUsers),
            Create(Role.Admin, Infrastructure.Permission.CreateProject),
            Create(Role.Admin, Infrastructure.Permission.UpdateProject),
            Create(Role.Admin, Infrastructure.Permission.DeleteProject),
            Create(Role.Admin, Infrastructure.Permission.ReadProject),
            Create(Role.Admin, Infrastructure.Permission.CreateBoard),
            Create(Role.Admin, Infrastructure.Permission.UpdateBoard),
            Create(Role.Admin, Infrastructure.Permission.DeleteBoard),
            Create(Role.Admin, Infrastructure.Permission.ReadBoard),
            Create(Role.Admin, Infrastructure.Permission.ManageAdmin),

        #endregion

        #region ProjectManagerRole

        Create(Role.ProjectManager, Infrastructure.Permission.CreateProject),
        Create(Role.ProjectManager, Infrastructure.Permission.UpdateProject),
        Create(Role.ProjectManager, Infrastructure.Permission.DeleteProject),
        Create(Role.ProjectManager, Infrastructure.Permission.ReadProject),
        Create(Role.ProjectManager, Infrastructure.Permission.ManageUsers),
        Create(Role.ProjectManager, Infrastructure.Permission.CreateBoard),
        Create(Role.ProjectManager, Infrastructure.Permission.UpdateBoard),
        Create(Role.ProjectManager, Infrastructure.Permission.DeleteBoard),
        Create(Role.ProjectManager, Infrastructure.Permission.ReadBoard),

        #endregion

        #region TeamMemberRole

        Create(Role.TeamMember, Infrastructure.Permission.ReadWorkSpace),
        Create(Role.TeamMember, Infrastructure.Permission.ReadProject),
        Create(Role.TeamMember, Infrastructure.Permission.ReadBoard),

        #endregion

        #region ViewerRole

        Create(Role.Viewer, Infrastructure.Permission.ReadWorkSpace),
        Create(Role.Viewer, Infrastructure.Permission.ReadProject),
        Create(Role.Viewer, Infrastructure.Permission.ReadBoard)

        #endregion
            );
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