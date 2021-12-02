using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Hero.Common.EntityFrameworkCore.Modeling;

public static class HeroEntityTypeBuilderExtensions
{
    public static void ConfigureByConvention(this EntityTypeBuilder b)
    {
        b.TryConfigureConcurrencyStamp();
        b.TryConfigureFullAuditedEntity();
        b.TryConfigureAuditedEntity();
    }

    public static void TryConfigureConcurrencyStamp(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo<IHasConcurrencyStamp>())
        {
            b.Property(nameof(IHasConcurrencyStamp.ConcurrencyStamp))
                .IsConcurrencyToken()
                .HasMaxLength(ConcurrencyStampConsts.MaxLength)
                .HasColumnName(nameof(IHasConcurrencyStamp.ConcurrencyStamp));
        }
    }

    public static void TryConfigureAuditedEntity(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo<AuditedEntity>())
        {
            b.Property(nameof(AuditedEntity.CreatedTime))
                .HasColumnName(nameof(FullAuditedEntity.CreatedTime));
            
            b.Property(nameof(AuditedEntity.UpdatedTime))
                .HasColumnName(nameof(FullAuditedEntity.UpdatedTime));

            b.Property(nameof(FullAuditedEntity.TenantId))
                .IsRequired(false)
                .HasColumnName(nameof(FullAuditedEntity.TenantId));
        }
    }

    public static void TryConfigureFullAuditedEntity(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo<FullAuditedEntity>())
        {
            b.Property(nameof(FullAuditedEntity.CreatedBy))
                .IsRequired(false)
                .HasColumnName(nameof(FullAuditedEntity.CreatedBy));
            b.Property(nameof(FullAuditedEntity.CreatedTime))
                .HasColumnName(nameof(FullAuditedEntity.CreatedTime));
            
            b.Property(nameof(FullAuditedEntity.UpdatedBy))
                .IsRequired(false)
                .HasColumnName(nameof(FullAuditedEntity.UpdatedBy));
            b.Property(nameof(FullAuditedEntity.UpdatedTime))
                .HasColumnName(nameof(FullAuditedEntity.UpdatedTime));

            b.Property(nameof(FullAuditedEntity.DeletedBy))
                .IsRequired(false)
                .HasColumnName(nameof(FullAuditedEntity.DeletedBy));
            b.Property(nameof(FullAuditedEntity.IsDeleted))
                .HasColumnName(nameof(FullAuditedEntity.IsDeleted));
            b.Property(nameof(FullAuditedEntity.DeletedTime))
                .IsRequired(false)
                .HasColumnName(nameof(FullAuditedEntity.DeletedTime));
            
            b.Property(nameof(FullAuditedEntity.TenantId))
                .IsRequired(false)
                .HasColumnName(nameof(FullAuditedEntity.TenantId));
        }
    }
}