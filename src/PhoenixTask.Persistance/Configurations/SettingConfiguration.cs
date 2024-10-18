using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Users;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(setting => setting.Key, keybuilder =>
        {
            keybuilder.WithOwner();

            keybuilder.Property(key => key.Value)
                .HasColumnName(nameof(Setting.Key))
                .HasMaxLength(Key.MaxLength)
                .IsRequired();
        });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.Property(setting => setting.Value)
            .IsRequired();
    }
}
