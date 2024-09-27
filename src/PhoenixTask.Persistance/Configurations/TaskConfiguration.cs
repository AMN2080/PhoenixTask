using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Projects;
using PhoenixTask.Domain.Users;
using PhoenixTask.Domain.Workspaces;
using Task = PhoenixTask.Domain.Tasks.Task;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(task => task.Id);

        builder.OwnsOne(task => task.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(Task.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatorId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(task => task.Description)
            .HasMaxLength(200);

        builder.Property(task => task.DeadLine);

        builder.Property(task => task.Priority);

        builder.Property(task => task.Order);

        builder.HasOne<Board>()
            .WithMany()
            .HasForeignKey(task=>task.BoardId);


        builder.Property(task => task.CreatedOnUtc).IsRequired();

        builder.Property(task => task.ModifiedOnUtc);

        builder.Property(task => task.DeletedOnUtc);

        builder.Property(task => task.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(task => !task.Deleted);
    }
}
