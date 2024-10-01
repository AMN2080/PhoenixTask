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

        builder.OwnsOne(user => user.FirstName, firstNameBuilder =>
        {
            firstNameBuilder.WithOwner();

            firstNameBuilder.Property(firstName => firstName.Value)
                .HasColumnName(nameof(User.FirstName))
                .HasMaxLength(FirstName.MaxLength)
                .IsRequired(false);
        });

        builder.OwnsOne(user => user.LastName, lastNameBuilder =>
        {
            lastNameBuilder.WithOwner();

            lastNameBuilder.Property(lastName => lastName.Value)
                .HasColumnName(nameof(User.LastName))
                .HasMaxLength(LastName.MaxLength)
                .IsRequired(false);
        });

        builder.OwnsOne(user => user.PhoneNumber, phoneNumberBuilder =>
        {
            phoneNumberBuilder.WithOwner();

            phoneNumberBuilder.Property(phoneNumber => phoneNumber.Value)
                .HasColumnName(nameof(User.PhoneNumber))
                .HasMaxLength(PhoneNumber.MaxLength)
                .IsRequired(false);
        });

        builder.Property<string>("_passwordHash")
                .HasField("_passwordHash")
                .HasColumnName("PasswordHash")
                .IsRequired();

        builder.Property<string>("_authKey")
            .HasField("_authKey")
            .HasColumnName("AuthKey")
            .IsRequired(false);

        builder.Property(user => user.CreatedOnUtc).IsRequired();

        builder.Property(user => user.ModifiedOnUtc);

        builder.Property(user => user.DeletedOnUtc);

        builder.Property(user => user.Deleted).HasDefaultValue(false);

        builder.Property(user => user.IsChangePassword);

        builder.HasQueryFilter(user => !user.Deleted);

    }
}
