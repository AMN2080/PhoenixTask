using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class WorkSpaceConfiguration : IEntityTypeConfiguration<WorkSpace>
{
    public void Configure(EntityTypeBuilder<WorkSpace> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(workspace => workspace.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(WorkSpace.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.Property(w=>w.Color)
                .HasColumnName(nameof(WorkSpace.Color))
                .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.OwnerId);
            

        builder.Property(workspace=> workspace.CreatedOnUtc).IsRequired();
                         
        builder.Property(workspace=> workspace.ModifiedOnUtc);
                         
        builder.Property(workspace=> workspace.DeletedOnUtc);
                         
        builder.Property(workspace=> workspace.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(workspace => !workspace.Deleted);
    }
}
