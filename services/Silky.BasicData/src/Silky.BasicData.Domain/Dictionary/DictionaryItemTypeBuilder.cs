using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;

namespace Silky.BasicData.Domain.Dictionary;

public class DictionaryItemTypeBuilder : IEntityTypeBuilder<DictionaryItem>
{
    public void Configure(EntityTypeBuilder<DictionaryItem> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(DictionaryDbProperties.DbTablePrefix + "DictionaryItems", DictionaryDbProperties.DbSchema);
        builder.ConfigureByConvention();
        
        
        builder.Property(e => e.Code)
            .HasMaxLength(DictionaryConsts.MaxCodeLength)
            .IsRequired()
            .HasColumnName(nameof(DictionaryType.Code));
        
        builder.Property(e => e.Value)
            .HasMaxLength(DictionaryConsts.MaxValueLength)
            .IsRequired()
            .HasColumnName(nameof(DictionaryType.Name));
        
        builder.Property(e => e.Remark)
            .HasMaxLength(DictionaryConsts.MaxRemarkLength)
            .HasColumnName(nameof(DictionaryType.Remark));
        
        builder.Property(e => e.Sort)
            .HasColumnName(nameof(DictionaryType.Sort));
        
    }
}