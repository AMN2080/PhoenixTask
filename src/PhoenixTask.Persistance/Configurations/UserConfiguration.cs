using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.OwnsOne(user => user.UserName, userNameBuilder =>
        {
            userNameBuilder.WithOwner();

            userNameBuilder.Property(userName => userName.Value)
                .HasColumnName(nameof(User.UserName))
                .HasMaxLength(UserName.MaxLength)
                .IsRequired();
        });

        builder.OwnsOne(user => user.Email, emailBuilder =>
        {
            emailBuilder.WithOwner();

            emailBuilder.Property(email => email.Value)
                .HasColumnName(nameof(User.Email))
                .HasMaxLength(Email.MaxLength)
                .IsRequired();
        });

        builder.Property<string>("_passwordHash")
                .HasField("_passwordHash")
                .HasColumnName("PasswordHash")
                .IsRequired();

        builder.Property(user => user.CreatedOnUtc).IsRequired();

        builder.Property(user => user.ModifiedOnUtc);

        builder.Property(user => user.DeletedOnUtc);

        builder.Property(user => user.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(user => !user.Deleted);

    }
}
