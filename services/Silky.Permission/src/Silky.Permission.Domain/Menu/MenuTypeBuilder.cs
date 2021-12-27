using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;

namespace Silky.Permission.Domain.Menu;

public class MenuTypeBuilder : IEntityTypeBuilder<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(PermissionDbProperties.DbTablePrefix + "Menus", PermissionDbProperties.DbSchema);
        builder.ConfigureByConvention();
        
        builder.Property(e => e.Name)
            .HasMaxLength(PermissionConsts.MaxNameLength)
            .IsRequired()
            .HasColumnName(nameof(Menu.Name));
        
        builder.Property(e => e.PermissionCode)
            .HasMaxLength(PermissionConsts.MaxCodeLength)
            .HasColumnName(nameof(Menu.PermissionCode));
        
        builder.Property(e => e.ParentId)
            .HasColumnName(nameof(Menu.ParentId));
        
        builder.Property(e => e.Icon)
            .HasMaxLength(PermissionConsts.MaxIconLength)
            .HasColumnName(nameof(Menu.Icon));
        
        builder.Property(e => e.Sort)
            .HasColumnName(nameof(Menu.Sort));
        
        builder.Property(e => e.RoutePath)
            .HasMaxLength(PermissionConsts.RoutePathLength)
            .HasColumnName(nameof(Menu.RoutePath));
        
        builder.Property(e => e.Component)
            .HasMaxLength(PermissionConsts.ComponentLength)
            .HasColumnName(nameof(Menu.Component));
        
        builder.Property(e => e.Status)
            .IsRequired()
            .HasColumnName(nameof(Menu.Status));
        
        builder.Property(e => e.ExternalLink)
            .HasColumnName(nameof(Menu.ExternalLink));
        
        builder.Property(e => e.Display)
            .HasColumnName(nameof(Menu.Display));
        
        builder.Property(e => e.Cache)
            .HasColumnName(nameof(Menu.Cache));

        builder.Property(e => e.Type)
            .IsRequired()
            .HasColumnName(nameof(Menu.Type));
    }
}