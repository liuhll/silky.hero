using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;
using Silky.Hero.Common.Enums;

namespace Silky.Position.Domain.Positions;

public class Position : FullAuditedEntity, IHasConcurrencyStamp, IEntityTypeBuilder<Position>
{
    public Position()
    {
        ConcurrencyStamp = Guid.NewGuid().ToString();
    }

    [NotNull] public string Name { get; set; }

    [NotNull] public string Code { get; set; }

    public int Sort { get; set; }

    public string Remark { get; set; }

    public Status Status { get; set; }
    public string ConcurrencyStamp { get; set; }

    public void Configure(EntityTypeBuilder<Position> entityBuilder, DbContext dbContext, Type dbContextLocator)
    {
        entityBuilder.ToTable(PositionDbProperties.DbTablePrefix + "Positions", PositionDbProperties.DbSchema);
        entityBuilder.ConfigureByConvention();

        entityBuilder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(PositionConsts.MaxNameLength)
            .HasColumnName(nameof(Name));
        entityBuilder.Property(o => o.Code)
            .IsRequired()
            .HasMaxLength(PositionConsts.MaxCodeLength)
            .HasColumnName(nameof(Code));
        entityBuilder.Property(o => o.Sort)
            .HasDefaultValue(0)
            .HasColumnName(nameof(Sort));
        entityBuilder.Property(o => o.Remark)
            .IsRequired()
            .HasMaxLength(PositionConsts.MaxRemarkLength)
            .HasColumnName(nameof(Remark));
        entityBuilder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(Status.Valid)
            .HasColumnName(nameof(Status));
    }
}