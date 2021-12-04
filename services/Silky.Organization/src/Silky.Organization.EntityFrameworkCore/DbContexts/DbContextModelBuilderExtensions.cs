using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Silky.Hero.Common.EntityFrameworkCore.Modeling;
using Silky.Hero.Common.Enums;
using Silky.Organization.Domain.Organizations;

namespace Silky.Organization.EntityFrameworkCore.DbContexts;

public static class DbContextModelBuilderExtensions
{
    public static void ConfigureOrganization([NotNull] this ModelBuilder builder)
    {
        builder.Entity<Domain.Organization>(b =>
        {
            b.ToTable(OrganizationDbProperties.DbTablePrefix + "Organizations", OrganizationDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(o => o.ParentId)
                .HasColumnName(nameof(Domain.Organization.ParentId));
            b.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(OrganizationConsts.MaxNameLength)
                .HasColumnName(nameof(Domain.Organization.Name));
            b.Property(o => o.Code)
                .IsRequired()
                .HasMaxLength(OrganizationConsts.MaxCodeLength)
                .HasColumnName(nameof(Domain.Organization.Code));
            b.Property(o => o.Sort)
                .HasDefaultValue(0)
                .HasColumnName(nameof(Domain.Organization.Sort));
            b.Property(o => o.Remark)
                .IsRequired()
                .HasMaxLength(OrganizationConsts.MaxRemarkLength)
                .HasColumnName(nameof(Domain.Organization.Remark));
            b.Property(o => o.Status)
                .IsRequired()
                .HasDefaultValue(Status.Valid)
                .HasColumnName(nameof(Domain.Organization.Status));
            b.HasMany(o => o.Children)
                .WithOne(o => o.Parent)
                .HasForeignKey(o => o.ParentId);
        });

        builder.Entity<Domain.OrganizationRole>(b =>
        {
            b.ToTable(OrganizationDbProperties.DbTablePrefix + "OrganizationRoles", OrganizationDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(o => o.OrganizationId)
                .IsRequired()
                .HasColumnName(nameof(Domain.OrganizationRole.OrganizationId));
            b.Property(o => o.RoleId)
                .IsRequired()
                .HasColumnName(nameof(Domain.OrganizationRole.RoleId));
        });
    }
}