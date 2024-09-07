using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Configurations;
internal sealed class WorkSpaceMemberConfiguration : IEntityTypeConfiguration<WorkSpaceMember>
{
    public void Configure(EntityTypeBuilder<WorkSpaceMember> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Roles)
            .WithMany()
            //.UsingEntity<WorkSpaceMemberRoleConfiguration>()
            ;

        builder.HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(x => x.CreatedOnUtc).IsRequired();

        builder.Property(x => x.ModifiedOnUtc);

        builder.Property(x => x.DeletedOnUtc);

        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(x => !x.Deleted);
    }
}
internal sealed class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Roles)
            .WithMany()
            //.UsingEntity<ProjectMemberRoleConfiguration>()
            ;

        builder.HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(x => x.CreatedOnUtc).IsRequired();

        builder.Property(x => x.ModifiedOnUtc);

        builder.Property(x => x.DeletedOnUtc);

        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(x => !x.Deleted);
    }
}
//internal sealed class ProjectMemberRoleConfiguration : IEntityTypeConfiguration<ProjectMemberRole>
//{
//    public void Configure(EntityTypeBuilder<ProjectMemberRole> builder)
//    {
//        builder.HasKey(x => new { x.ProjectMemberId, x.RoleId });
//    }
//}
//internal sealed class WorkSpaceMemberRoleConfiguration : IEntityTypeConfiguration<WorkSpaceMemberRole>
//{
//    public void Configure(EntityTypeBuilder<WorkSpaceMemberRole> builder)
//    {
//        builder.HasKey(x => new { x.WorkSpaceMemberId, x.RoleId });
//    }
//}