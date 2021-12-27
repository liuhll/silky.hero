using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;

namespace Silky.Permission.Domain;

public class PermissionTypeBuilder : IEntityTypeBuilder<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(PermissionDbProperties.DbTablePrefix + "Permissions", PermissionDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.Property(p => p.Code)
            .HasMaxLength(PermissionConsts.MaxCodeLength)
            .IsRequired()
            .HasColumnName(nameof(Permission.Code));
        
        builder.Property(p => p.HostName)
            .HasMaxLength(PermissionConsts.MaxNameLength)
            .HasColumnName(nameof(Permission.HostName));
        
        builder.Property(p => p.ServiceName)
            .HasMaxLength(PermissionConsts.MaxNameLength)
            .HasColumnName(nameof(Permission.ServiceName));
        
        builder.Property(p => p.ServiceEntryId)
            .HasMaxLength(PermissionConsts.MaxServiceEntryIdLength)
            .HasColumnName(nameof(Permission.ServiceEntryId));
        
        builder.Property(p => p.Method)
            .HasMaxLength(PermissionConsts.MaxMethodLength)
            .HasColumnName(nameof(Permission.Method));
        
        builder.Property(p => p.HttpMethod)
            .HasMaxLength(PermissionConsts.MaxMethodLength)
            .HasColumnName(nameof(Permission.HttpMethod));
        
        builder.Property(p => p.WebApi)
            .HasMaxLength(PermissionConsts.MaxWebApiLength)
            .HasColumnName(nameof(Permission.WebApi));
    }
}