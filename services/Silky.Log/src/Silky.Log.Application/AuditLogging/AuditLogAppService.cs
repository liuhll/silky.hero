using System.Threading.Tasks;
using Mapster;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Log.Application.Contracts.AuditLogging;
using Silky.Log.Domain.AuditLogging;
using Silky.Rpc.Auditing;

namespace Silky.Log.Application.AuditLogging;

public class AuditLogAppService : IAuditLogAppService
{
    private readonly IRepository<AuditLog> _auditLogRepository;

    public AuditLogAppService(IRepository<AuditLog> auditLogRepository)
    {
        _auditLogRepository = auditLogRepository;
    }

    public async Task SaveAsync(AuditLogInfo auditLogInfo)
    {
        var auditLog = auditLogInfo.Adapt<AuditLog>();
        await _auditLogRepository.InsertAsync(auditLog);
    }
}