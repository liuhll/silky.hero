using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Silky.Core.Logging;
using Silky.Http.Auditing;
using Silky.Rpc.Auditing;

namespace Silky.GatewayHost.AuditLogging;

public class HeroAuditingStore : IAuditingStore
{
    private readonly ILogger<HeroAuditingStore> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public HeroAuditingStore(
        ILogger<HeroAuditingStore> logger,
        IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task SaveAsync(AuditLogInfo auditLogInfo)
    {
        try
        {
            await _publishEndpoint.Publish(auditLogInfo);
        }
        catch (Exception e)
        {
            _logger.LogWarning("保存审计日志失败");
            _logger.LogException(e);
        }
    }
}