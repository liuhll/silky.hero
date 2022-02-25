using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using org.apache.zookeeper;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;
using Silky.Hero.Common.Enums;

namespace Silky.Position.Domain;

public class PositionTypeBuilder : IEntityTypeBuilder<Position>
{
    public void Configure(EntityTypeBuilder<Position> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.ToTable(PositionDbProperties.DbTablePrefix + "Positions", PositionDbProperties.DbSchema);
        entityBuilder.ConfigureByConvention();

        entityBuilder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(PositionConsts.MaxNameLength)
            .HasColumnName(nameof(Position.Name));
        entityBuilder.Property(o => o.Sort)
            .HasDefaultValue(0)
            .HasColumnName(nameof(Position.Sort));
        entityBuilder.Property(o => o.Remark)
            .HasMaxLength(PositionConsts.MaxRemarkLength)
            .HasColumnName(nameof(Position.Remark));
        
        entityBuilder.Property(o => o.IsStatic)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName(nameof(Position.IsStatic));
        
        entityBuilder.Property(o => o.IsPublic)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName(nameof(Position.IsPublic));
        
        entityBuilder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(Status.Valid)
            .HasColumnName(nameof(Status));
    }
}