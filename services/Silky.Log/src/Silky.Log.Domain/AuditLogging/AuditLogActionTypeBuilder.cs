using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.Extras.Modeling;
using Silky.Log.Domain.Shared.AuditLogging;

namespace Silky.Log.Domain.AuditLogging;

public class AuditLogActionTypeBuilder : IEntityTypeBuilder<AuditLogAction>
{
    public void Configure(EntityTypeBuilder<AuditLogAction> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(AuditLoggingDbProperties.DbTablePrefix + "AuditLogActions", AuditLoggingDbProperties.DbSchema);
        builder.ConfigureByConvention();
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(o => o.HostName)
            .IsRequired()
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLogAction.HostName));

        builder.Property(o => o.ServiceEntryId)
            .IsRequired()
            .HasMaxLength(AuditLoggingConsts.LengthFor512)
            .HasColumnName(nameof(AuditLogAction.ServiceEntryId));

        builder.Property(o => o.HostAddress)
            .IsRequired()
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLogAction.HostAddress));

        builder.Property(o => o.ServiceName)
            .IsRequired()
            .HasMaxLength(AuditLoggingConsts.LengthFor256)
            .HasColumnName(nameof(AuditLogAction.ServiceName));

        builder.Property(o => o.ServiceKey)
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLogAction.ServiceKey));

        builder.Property(o => o.MethodName)
            .IsRequired()
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLogAction.MethodName));

        builder.Property(o => o.Parameters)
            .HasMaxLength(AuditLoggingConsts.LengthFor512)
            .HasColumnName(nameof(AuditLogAction.Parameters));

        builder.Property(o => o.IsDistributedTransaction)
            .HasColumnName(nameof(AuditLogAction.IsDistributedTransaction));

        builder.Property(o => o.ExceptionMessage)
            .HasMaxLength(AuditLoggingConsts.LengthFor1024)
            .HasColumnName(nameof(AuditLogAction.ExceptionMessage));

        builder.Property(o => o.ExecutionDuration)
            .IsRequired()
            .HasColumnName(nameof(AuditLogAction.ExecutionDuration));

        builder.Property(o => o.ExecutionTime)
            .IsRequired()
            .HasColumnName(nameof(AuditLogAction.ExecutionTime));
    }
}