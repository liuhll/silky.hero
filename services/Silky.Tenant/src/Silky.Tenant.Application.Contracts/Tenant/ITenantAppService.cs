using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Tenant.Application.Contracts.Tenant.Dtos;

namespace Silky.Tenant.Application.Contracts.Tenant;

/// <summary>
/// 租户信息服务
/// </summary>
[ServiceRoute]
public interface ITenantAppService
{
    /// <summary>
    /// 新增或更新租户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [HttpPost]
    [RemoveCachingIntercept(typeof(GetTenantOutput),"id:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetTenantOutput>),"all")]
    Task CreateOrUpdateAsync(CreateOrUpdateTenantInput input);

    /// <summary>
    /// 通过Id获取租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetTenantOutput> GetAsync([CacheKey(0)]long id);

    /// <summary>
    /// 通过id删除租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetTenantOutput),"id:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetTenantOutput>),"all")]
    Task DeleteAsync([CacheKey(0)]long id);

    /// <summary>
    /// 分页查询租户信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetTenantPageOutput>> GetPageAsync(GetTenantPageInput input);
    
    /// <summary>
    /// 获取有效租户信息接口
    /// </summary>
    /// <returns></returns>
    [GetCachingIntercept("all")]
    Task<ICollection<GetTenantOutput>> GetAllAsync();
}