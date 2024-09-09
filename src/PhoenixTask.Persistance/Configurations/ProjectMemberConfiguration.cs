using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Projects;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Roles)
            .WithMany();

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