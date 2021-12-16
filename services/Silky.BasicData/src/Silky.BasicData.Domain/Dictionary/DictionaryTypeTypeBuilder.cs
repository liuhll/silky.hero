using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;

namespace Silky.BasicData.Domain.Dictionary;

public class DictionaryTypeTypeBuilder : IEntityTypeBuilder<DictionaryType>
{
    public void Configure(EntityTypeBuilder<DictionaryType> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(DictionaryDbProperties.DbTablePrefix + "DictionaryTypes", DictionaryDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.Property(e => e.Code)
            .HasMaxLength(DictionaryConsts.MaxCodeLength)
            .IsRequired()
            .HasColumnName(nameof(DictionaryType.Code));

        builder.Property(e => e.Name)
            .HasMaxLength(DictionaryConsts.MaxNameLength)
            .IsRequired()
            .HasColumnName(nameof(DictionaryType.Name));

        builder.Property(e => e.Remark)
            .HasMaxLength(DictionaryConsts.MaxRemarkLength)
            .HasColumnName(nameof(DictionaryType.Remark));

        builder.Property(e => e.Sort)
            .HasColumnName(nameof(DictionaryType.Sort));

        builder.HasMany(e => e.DictionaryItems)
            .WithOne(n => n.DictionaryType)
            .HasForeignKey(i => i.DictionaryId)
            .IsRequired();
    }
}