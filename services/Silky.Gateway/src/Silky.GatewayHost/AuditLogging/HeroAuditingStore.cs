using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Silky.Core.Logging;
using Silky.Http.Auditing;
using Silky.Log.Application.Contracts.AuditLogging;
using Silky.Rpc.Auditing;
using Silky.Rpc.Security;

namespace Silky.GatewayHost.AuditLogging;

public class HeroAuditingStore : IAuditingStore
{
    private readonly IAuditLogAppService _auditLogAppService;
    private readonly ICurrentRpcToken _currentRpcToken;
    private readonly ILogger<HeroAuditingStore> _logger;

    public HeroAuditingStore(IAuditLogAppService auditLogAppService,
        ICurrentRpcToken currentRpcToken,
        ILogger<HeroAuditingStore> logger)
    {
        _auditLogAppService = auditLogAppService;
        _currentRpcToken = currentRpcToken;
        _logger = logger;
    }

    public async Task SaveAsync(AuditLogInfo auditLogInfo)
    {
        try
        {
            // _currentRpcToken.SetRpcToken();
            // await _auditLogAppService.SaveAsync(auditLogInfo);
        }
        catch (Exception e)
        {
            _logger.LogWarning("保存审计日志失败");
            _logger.LogException(e);
        }
    }
}