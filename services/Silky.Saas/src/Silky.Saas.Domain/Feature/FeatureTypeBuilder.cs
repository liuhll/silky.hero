using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;
using Silky.Hero.Common.Extensions;
using Silky.Saas.Domain.Shared;

namespace Silky.Saas.Domain;

public class FeatureTypeBuilder : IEntityTypeBuilder<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(TenantDbProperties.DbTablePrefix + "Features", TenantDbProperties.DbSchema);
        builder.ConfigureByConvention();
        
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(SaasConsts.MaxNameLength)
            .HasColumnName(nameof(Feature.Name));
        
        builder.Property(o => o.Code)
            .IsRequired()
            .HasMaxLength(SaasConsts.MaxCodeLength)
            .HasColumnName(nameof(Feature.Code));
        
        builder.Property(o => o.Description)
            .HasMaxLength(SaasConsts.MaxDescriptionLength)
            .HasColumnName(nameof(Feature.Description));
        
        builder.Property(o => o.FeatureType)
            .IsRequired()
            .HasColumnName(nameof(Feature.FeatureType));
        
        builder.Property(o => o.Options)
            .HasConversion(
                u => u.ToJsonString(false, false), 
                u => u.FromJsonString<ICollection<FeatureOption>>())
            .HasColumnName(nameof(Feature.Options));
        
        builder.HasMany(p => p.EditionFeatures)
            .WithOne(p => p.Feature)
            .HasForeignKey(p => p.FeatureId);
    }
}