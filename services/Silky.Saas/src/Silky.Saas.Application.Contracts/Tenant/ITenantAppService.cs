using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;
using Silky.Saas.Application.Contracts.Tenant.Dtos;
using Silky.Saas.Domain.Shared;
using Silky.Transaction;

namespace Silky.Saas.Application.Contracts.Tenant;

/// <summary>
/// 租户信息服务
/// </summary>
[ServiceRoute]
[Authorize]
public interface ITenantAppService
{
    /// <summary>
    /// 新增租户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(ICollection<GetTenantOutput>), "all", IgnoreMultiTenancy = true)]
    [Authorize(SaasPermissions.Tenants.Create)]
    [Transaction]
    Task CreateAsync(CreateTenantInput input);

    /// <summary>
    /// 更新组件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetTenantOutput), "id:{Id}", IgnoreMultiTenancy = true)]
    [RemoveMatchKeyCachingIntercept("tenantName:*", IgnoreMultiTenancy = true)]
    [RemoveCachingIntercept(typeof(ICollection<GetTenantOutput>), "all", IgnoreMultiTenancy = true)]
    [Authorize(SaasPermissions.Tenants.Update)]
    Task UpdateAsync(UpdateTenantInput input);

    /// <summary>
    /// 检查租户是否存在
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("check")]
    Task<bool> CheckAsync(CheckTenantInput input);

    /// <summary>
    /// 通过Id获取租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{id}", IgnoreMultiTenancy = true)]
    [Authorize(SaasPermissions.Tenants.LookDetail)]
    Task<GetTenantOutput> GetAsync(long id);

    /// <summary>
    /// 通过id删除租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetTenantOutput), "id:{id}", IgnoreMultiTenancy = true)]
    [RemoveCachingIntercept(typeof(ICollection<GetTenantOutput>), "all", IgnoreMultiTenancy = true)]
    [RemoveMatchKeyCachingIntercept("tenantName:*", IgnoreMultiTenancy = true)]
    [Authorize(SaasPermissions.Tenants.Delete)]
    Task DeleteAsync(long id);

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
    [AllowAnonymous]
    Task<ICollection<GetTenantOutput>> GetAllAsync();
    
    /// <summary>
    /// 通过租户标识获取租户信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [ProhibitExtranet]
    [GetCachingIntercept("tenantName:{name}", IgnoreMultiTenancy = true)]
    Task<GetTenantOutput> GetByNameAsync(string name);
}