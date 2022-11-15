using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.Extras.Modeling;
using Silky.Saas.Domain.Shared;

namespace Silky.Saas.Domain;

public class EditionFeatureTypeBuilder : IEntityTypeBuilder<EditionFeature>
{
    public void Configure(EntityTypeBuilder<EditionFeature> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(TenantDbProperties.DbTablePrefix + "EditionFeatures", TenantDbProperties.DbSchema);
        builder.ConfigureByConvention();
        
        builder.Property(o => o.EditionId)
            .IsRequired()
            .HasColumnName(nameof(EditionFeature.EditionId));
        
        builder.Property(o => o.FeatureId)
            .IsRequired()
            .HasColumnName(nameof(EditionFeature.FeatureId));
        
        builder.Property(o => o.FeatureValue)
            .IsRequired()
            .HasMaxLength(SaasConsts.MaxFeatureValueLength)
            .HasColumnName(nameof(EditionFeature.FeatureValue));
    }
}