using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Log.Application.Contracts.AuditLogging.Dtos;
using Silky.Rpc.Auditing;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

namespace Silky.Log.Application.Contracts.AuditLogging;

[ServiceRoute]
public interface IAuditLogAppService
{
    /// <summary>
    /// 保存审计日志
    /// </summary>
    /// <param name="auditLogInfo"></param>
    /// <returns></returns>
    [Governance(ProhibitExtranet = true)]
    Task SaveAsync(AuditLogInfo auditLogInfo);

    /// <summary>
    /// 分页查询审计日志
    /// </summary>
    /// <param name="input">查询输入参数</param>
    /// <returns></returns>
    Task<PagedList<GetAuditLogPageOutput>> GetPageAsync(GetAuditLogPageInput input);
}