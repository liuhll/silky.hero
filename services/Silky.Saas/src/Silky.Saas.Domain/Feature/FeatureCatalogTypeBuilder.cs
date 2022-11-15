using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.Extras.Modeling;
using Silky.Saas.Domain.Shared;

namespace Silky.Saas.Domain;

public class FeatureCatalogTypeBuilder : IEntityTypeBuilder<FeatureCatalog>
{
    public void Configure(EntityTypeBuilder< FeatureCatalog> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(TenantDbProperties.DbTablePrefix + "FeatureCatalogs", TenantDbProperties.DbSchema);
        builder.ConfigureByConvention();
        
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(SaasConsts.MaxNameLength)
            .HasColumnName(nameof(FeatureCatalog.Name));

        builder.HasMany(p => p.Features)
            .WithOne(p => p.FeatureCatalog)
            .HasForeignKey(p => p.FeatureCatalogId);

    }
}