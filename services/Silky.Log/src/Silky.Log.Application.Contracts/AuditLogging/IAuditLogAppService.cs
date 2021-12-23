﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Log.Application.Contracts.AuditLogging.Dtos;
using Silky.Rpc.Auditing;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

namespace Silky.Log.Application.Contracts.AuditLogging;

/// <summary>
/// 审计日志服务
/// </summary>
[ServiceRoute]
[DisableAuditing]
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

    /// <summary>
    /// 通过Id获取审计日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetAuditLogOutput> GetAsync([CacheKey(0)] long id);
}