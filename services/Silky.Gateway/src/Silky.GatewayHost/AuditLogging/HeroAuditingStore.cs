using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Silky.Core.Logging;
using Silky.Http.Auditing;
using Silky.Http.Core;
using Silky.Log.Application.Contracts.AuditLogging;
using Silky.Rpc.Auditing;
using Silky.Rpc.Extensions;

namespace Silky.GatewayHost.AuditLogging;

public class HeroAuditingStore : IAuditingStore
{
    private readonly IAuditLogAppService _auditLogAppService;
    private readonly ILogger<HeroAuditingStore> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HeroAuditingStore(IAuditLogAppService auditLogAppService,
        ILogger<HeroAuditingStore> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _auditLogAppService = auditLogAppService;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SaveAsync(AuditLogInfo auditLogInfo)
    {
        try
        {
            var serviceEntry = _httpContextAccessor.HttpContext.GetServiceEntry();
            var requestParameters = serviceEntry.GetParameters(_httpContextAccessor.HttpContext.Request);
            await _auditLogAppService.SaveAsync(auditLogInfo, requestParameters);
        }
        catch (Exception e)
        {
            _logger.LogWarning("保存审计日志失败");
            _logger.LogException(e);
        }
    }
}