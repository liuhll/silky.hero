using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.Extras.Modeling;
using Silky.Saas.Domain.Shared;

namespace Silky.Saas.Domain;

public class EditionTypeBuilder : IEntityTypeBuilder<Edition>
{
    public void Configure(EntityTypeBuilder<Edition> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(TenantDbProperties.DbTablePrefix + "Editions", TenantDbProperties.DbSchema);
        builder.ConfigureByConvention();
        
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(SaasConsts.MaxNameLength)
            .HasColumnName(nameof(Edition.Name));
        
        builder.Property(o => o.Price)
            .HasColumnName(nameof(Edition.Price));
        
        builder.Property(o => o.Sort)
            .HasColumnName(nameof(Edition.Sort));
        
        builder.Property(o => o.Remark)
            .HasMaxLength(SaasConsts.MaxRemarkLength)
            .HasColumnName(nameof(Edition.Remark));
        
        builder.HasMany(p => p.EditionFeatures)
            .WithOne(p => p.Edition)
            .HasForeignKey(p => p.EditionId);
        
        builder.HasMany(p => p.Tenants)
            .WithOne(p => p.Edition)
            .HasForeignKey(p => p.EditionId);
    }
}