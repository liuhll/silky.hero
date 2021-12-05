using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;

namespace Silky.Identity.Application.Contracts.Role;

/// <summary>
/// 角色信息服务
/// </summary>
[ServiceRoute]
public interface IRoleAppService
{
    /// <summary>
    /// 新增/更新角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    [RemoveCachingIntercept(typeof(GetRoleOutput), "id:{0}")]
    Task CreateOrUpdateAsync(CreateOrUpdateRoleInput input);

    /// <summary>
    /// 根据id获取角色信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetRoleOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 根据Id删除角色信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetRoleOutput), "id:{0}")]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 分页查询角色信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetRolePageOutput>> GetPageAsync(GetRolePageInput input);
}