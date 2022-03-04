using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;
using Silky.Hero.Common.Enums;
using Silky.Saas.Domain.Shared;

namespace Silky.Saas.Domain;

public class TenantTypeBuilder : IEntityTypeBuilder<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(TenantDbProperties.DbTablePrefix + "Tenants", TenantDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(SaasConsts.MaxNameLength)
            .HasColumnName(nameof(Tenant.Name));
        
        builder.Property(o => o.RealName)
            .IsRequired()
            .HasMaxLength(SaasConsts.MaxRealNameLength)
            .HasColumnName(nameof(Tenant.RealName));
        
        builder.Property(o => o.Remark)
            .HasMaxLength(SaasConsts.MaxRemarkLength)
            .HasColumnName(nameof(Tenant.Remark));
        
        builder.Property(o => o.Sort)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnName(nameof(Tenant.Sort));
        
        builder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(Status.Valid)
            .HasColumnName(nameof(Tenant.Status));

        builder.Property(p => p.EditionId)
            .IsRequired()
            .HasColumnName(nameof(Tenant.EditionId));
    }
}