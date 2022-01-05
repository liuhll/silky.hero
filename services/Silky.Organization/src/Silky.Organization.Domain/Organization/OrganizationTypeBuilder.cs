using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;
using Silky.Hero.Common.Enums;

namespace Silky.Organization.Domain;

public class OrganizationTypeBuilder : IEntityTypeBuilder<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(OrganizationDbProperties.DbTablePrefix + "Organizations", OrganizationDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.Property(o => o.ParentId)
            .HasColumnName(nameof(Domain.Organization.ParentId));
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(OrganizationConsts.MaxNameLength)
            .HasColumnName(nameof(Domain.Organization.Name));
        builder.Property(o => o.Code)
            .IsRequired()
            .HasMaxLength(OrganizationConsts.MaxCodeLength)
            .HasColumnName(nameof(Domain.Organization.Code));
        builder.Property(o => o.Sort)
            .HasDefaultValue(0)
            .HasColumnName(nameof(Domain.Organization.Sort));
        builder.Property(o => o.Remark)
            .HasMaxLength(OrganizationConsts.MaxRemarkLength)
            .HasColumnName(nameof(Domain.Organization.Remark));
        builder.Property(o => o.Status)
            .IsRequired()
            .HasDefaultValue(Status.Valid)
            .HasColumnName(nameof(Domain.Organization.Status));
        builder.HasMany(o => o.Children)
            .WithOne(o => o.Parent)
            .HasForeignKey(o => o.ParentId);
    }
}