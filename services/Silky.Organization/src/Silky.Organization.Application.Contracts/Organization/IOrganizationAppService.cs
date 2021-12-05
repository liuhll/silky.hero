using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

namespace Silky.Organization.Application.Contracts.Organization;

/// <summary>
/// 组织机构服务
/// </summary>
[ServiceRoute]
public interface IOrganizationAppService
{
    /// <summary>
    /// 新增/更新组织机构
    /// </summary>
    /// <remarks>如果Id为null，则表示新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    [RemoveCachingIntercept(typeof(GetOrganizationOutput),"id:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetOrganizationTreeOutput>),"tree")]
    [RemoveCachingIntercept(typeof(bool),"HasOrganization:{0}")]
    Task CreateOrUpdateAsync(CreateOrUpdateOrganizationInput input);

    /// <summary>
    /// 删除组织机构
    /// </summary>
    /// <param name="id">主键Id</param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetOrganizationOutput),"id:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetOrganizationTreeOutput>),"tree")]
    [RemoveCachingIntercept(typeof(bool),"HasOrganization:{0}")]
    Task DeleteAsync([CacheKey(0)]long id);

    /// <summary>
    /// 通过Id获取组织机构
    /// </summary>
    /// <param name="id">主键Id</param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetOrganizationOutput> GetAsync([CacheKey(0)]long id);
    
    /// <summary>
    /// 分页获取组织机构信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetOrganizationPageOutput>> GetPageAsync(GetOrganizationPageInput input);

    /// <summary>
    /// 获取组织机构树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [GetCachingIntercept("tree")]
    Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync();

    /// <summary>
    /// 判断是否存在组织机构
    /// </summary>
    /// <param name="organizationId">组织机构id</param>
    /// <returns></returns>
    [Governance(ProhibitExtranet = true)]
    [GetCachingIntercept("HasOrganization:{0}")]
    Task<bool> HasOrganizationAsync([CacheKey(0)]long organizationId);
}