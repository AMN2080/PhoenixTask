﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoenixTask.Domain.Authorities;

namespace PhoenixTask.Persistance.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Domain.Authorities.Permission>
{
    public void Configure(EntityTypeBuilder<Domain.Authorities.Permission> builder)
    {
        builder.HasKey(x => x.Id);

        var permissions = Enum.GetValues<PermissionType>()
            .Select(p => new Domain.Authorities.Permission
            {
                Id = (int)p,
                Name = p.ToString()
            });

        builder.HasData(permissions);
    }
}
