using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;

namespace Silky.Organization.Domain;

public class OrganizationPositionTypeBuilder  : IEntityTypeBuilder<OrganizationPosition>
{
    public void Configure(EntityTypeBuilder<OrganizationPosition> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(OrganizationDbProperties.DbTablePrefix + "OrganizationPositions", OrganizationDbProperties.DbSchema);
        builder.ConfigureByConvention();
        
        builder.Property(o => o.OrganizationId)
            .IsRequired()
            .HasColumnName(nameof(Domain.OrganizationPosition.OrganizationId));
        
        builder.Property(o => o.PositionId)
            .IsRequired()
            .HasColumnName(nameof(Domain.OrganizationPosition.PositionId));
    }
}