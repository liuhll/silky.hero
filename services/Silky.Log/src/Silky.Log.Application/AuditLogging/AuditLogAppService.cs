using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Log.Application.Contracts.AuditLogging;
using Silky.Log.Application.Contracts.AuditLogging.Dtos;
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

    public async Task<PagedList<GetAuditLogPageOutput>> GetPageAsync(GetAuditLogPageInput input)
    {
        var auditLogPage = await _auditLogRepository
            .Where(input.StartTime.HasValue, p => p.ExecutionTime >= input.StartTime)
            .Where(input.EndTime.HasValue, p => p.ExecutionTime <= input.EndTime)
            .Where(input.MaxExecutionDuration.HasValue, p => p.ExecutionDuration <= input.MaxExecutionDuration)
            .Where(input.MinExecutionDuration.HasValue, p => p.ExecutionDuration >= input.MinExecutionDuration)
            .Where(!input.Url.IsNullOrEmpty(), p => p.Url.Contains(input.Url))
            .Where(!input.UserName.IsNullOrEmpty(),
                p => p.UserName.Contains(input.UserName))
            .Where(!input.HttpMethod.IsNullOrEmpty(),
                p => p.HttpMethod.Equals(input.HttpMethod))
            .Where(input.HttpStatusCode.HasValue, p => p.HttpStatusCode.Equals(input.HttpStatusCode))
            .Where(input.HasException.HasValue, p => p.ExceptionMessage != null)
            .OrderByDescending(p=> p.ExecutionTime)
            .ProjectToType<GetAuditLogPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return auditLogPage;
    }
}