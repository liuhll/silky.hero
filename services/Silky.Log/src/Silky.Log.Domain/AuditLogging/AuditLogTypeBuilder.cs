using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.EntityFrameworkCore.Extras.Modeling;
using Silky.Log.Domain.Shared.AuditLogging;

namespace Silky.Log.Domain.AuditLogging;

public class AuditLogTypeBuilder : IEntityTypeBuilder<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder, DbContext dbContext, Type dbContextLocator)
    {
        builder.ToTable(AuditLoggingDbProperties.DbTablePrefix + "AuditLogs", AuditLoggingDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UserId)
            .HasColumnName(nameof(AuditLog.UserId));

        builder.Property(o => o.UserName)
            .HasMaxLength(AuditLoggingConsts.UserNameMaxLength)
            .HasColumnName(nameof(AuditLog.UserName));

        builder.Property(o => o.TenantId)
            .HasColumnName(nameof(AuditLog.TenantId));

        builder.Property(o => o.ExecutionDuration)
            .IsRequired()
            .HasColumnName(nameof(AuditLog.ExecutionDuration));

        builder.Property(o => o.ExecutionTime)
            .IsRequired()
            .HasColumnName(nameof(AuditLog.ExecutionTime));

        builder.Property(o => o.BrowserInfo)
            .HasMaxLength(AuditLoggingConsts.LengthFor512)
            .HasColumnName(nameof(AuditLog.BrowserInfo));

        builder.Property(o => o.Url)
            .IsRequired()
            .HasMaxLength(AuditLoggingConsts.LengthFor256)
            .HasColumnName(nameof(AuditLog.Url));

        builder.Property(o => o.HttpMethod)
            .IsRequired()
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLog.HttpMethod));

        builder.Property(o => o.ClientIpAddress)
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLog.ClientIpAddress));

        builder.Property(o => o.ClientId)
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLog.ClientId));

        builder.Property(o => o.CorrelationId)
            .HasMaxLength(AuditLoggingConsts.LengthFor100)
            .HasColumnName(nameof(AuditLog.CorrelationId));

        builder.Property(o => o.HttpStatusCode)
            .HasColumnName(nameof(AuditLog.HttpStatusCode));
        
        builder.Property(o => o.RequestParameters)
            .HasMaxLength(AuditLoggingConsts.LengthFor1024)
            .HasColumnName(nameof(AuditLog.RequestParameters));

        builder.Property(o => o.ExceptionMessage)
            .HasMaxLength(AuditLoggingConsts.LengthFor1024)
            .HasColumnName(nameof(AuditLog.ExceptionMessage));

        builder.HasMany(p => p.Actions)
            .WithOne(p => p.AuditLog)
            .HasForeignKey(f => f.AuditLogId)
           ;
    }
}