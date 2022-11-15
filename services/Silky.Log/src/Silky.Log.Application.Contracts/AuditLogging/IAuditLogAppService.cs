using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Log.Application.Contracts.AuditLogging.Dtos;
using Silky.Log.Domain.Shared;
using Silky.Rpc.Auditing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Routing;
using Silky.Rpc.Security;

namespace Silky.Log.Application.Contracts.AuditLogging;

/// <summary>
/// 审计日志服务
/// </summary>
[ServiceRoute]
[DisableAuditing]
[Authorize]
public interface IAuditLogAppService
{
    
    /// <summary>
    /// 分页查询审计日志
    /// </summary>
    /// <param name="input">查询输入参数</param>
    /// <returns></returns>
   // [Authorize(LogPermissions.AuditLogging.Search)]
    Task<PagedList<GetAuditLogPageOutput>> GetPageAsync(GetAuditLogPageInput input);

    /// <summary>
    /// 通过Id获取审计日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{id}")]
    [Authorize(LogPermissions.AuditLogging.LookDetail)]
    Task<GetAuditLogOutput> GetAsync(long id);
}