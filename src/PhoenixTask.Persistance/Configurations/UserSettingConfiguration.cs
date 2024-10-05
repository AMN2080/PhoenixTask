using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class UserSettingConfiguration : IEntityTypeConfiguration<UserSetting>
{
    public void Configure(EntityTypeBuilder<UserSetting> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.OwnsOne(x => x.Key, keybuilder =>
        {
            keybuilder.WithOwner();

            keybuilder.Property(key => key.Value)
            .HasColumnName(nameof(UserSetting.Key))
            .HasMaxLength(Key.MaxLength)
            .IsRequired();

        });

        builder.Property(x => x.Value)
            .IsRequired();
    }
}
