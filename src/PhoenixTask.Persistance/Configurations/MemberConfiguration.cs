using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Authorities;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Configurations;
internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasDiscriminator(member => member.MemberType)
            .HasValue<WorkSpaceMember>(MemberType.WorkSpaceMember)
            .HasValue<ProjectMember>(MemberType.ProjectMember);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(x => x.RoleValue)
            .IsRequired();


        builder.Property(x => x.CreatedOnUtc).IsRequired();

        builder.Property(x => x.ModifiedOnUtc);

        builder.Property(x => x.DeletedOnUtc);

        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(x => !x.Deleted);
    }
}