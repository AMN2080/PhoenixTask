using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Workspaces;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(board => board.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(Board.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.Property(b => b.Color)
                .HasColumnName(nameof(Board.Color))
                .IsRequired();

        builder.Property(b => b.Order)
            .HasColumnName(nameof(Board.Order))
            .HasDefaultValue(0)
            .IsRequired();

        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(x => x.ProjectId);

        builder.Property(project => project.CreatedOnUtc).IsRequired();

        builder.Property(project => project.ModifiedOnUtc);

        builder.Property(project => project.DeletedOnUtc);

        builder.Property(project => project.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(project => !project.Deleted);
    }
}