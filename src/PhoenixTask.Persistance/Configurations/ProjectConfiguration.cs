using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(project => project.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(Project.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.HasOne<WorkSpace>()
            .WithMany()
            .HasForeignKey(x => x.WorkSpaceId);

        builder.Property(project => project.CreatedOnUtc).IsRequired();

        builder.Property(project => project.ModifiedOnUtc);

        builder.Property(project => project.DeletedOnUtc);

        builder.Property(project => project.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(project => !project.Deleted);
    }
}
