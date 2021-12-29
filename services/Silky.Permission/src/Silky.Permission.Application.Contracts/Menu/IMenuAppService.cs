using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;

namespace Silky.Permission.Application.Contracts.Menu;

/// <summary>
/// 菜单服务
/// </summary>
[ServiceRoute]
public interface IMenuAppService
{
    /// <summary>
    /// 新增/更新菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    [RemoveCachingIntercept(typeof(GetMenuOutput),"id:{0}")]
    Task CreateOrUpdateAsync(CreateOrUpdateMenuInput input);

    /// <summary>
    /// 通过Id获取菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [GetCachingIntercept("id:{0}")]
    Task<GetMenuOutput> GetAsync([CacheKey(0)]long id);

    /// <summary>
    /// 根据id删除菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetMenuOutput),"id:{0}")]
    Task DeleteAsync([CacheKey(0)]long id);

    /// <summary>
    /// 分页获取菜单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetMenuPageOutput>> GetPageAsync(GetMenuPageInput input);
}