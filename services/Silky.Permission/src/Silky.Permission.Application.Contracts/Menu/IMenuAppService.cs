using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Permission.Domain.Shared;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Security;

namespace Silky.Permission.Application.Contracts.Menu;

/// <summary>
/// 菜单服务
/// </summary>
[ServiceRoute]
[Authorize(PermissionPermissions.Menus.Default)]
public interface IMenuAppService
{
    /// <summary>
    /// 新增菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(PermissionPermissions.Menus.Create)]
    Task CreateAsync(CreateMenuInput input);

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetMenuOutput), "id:{0}")]
    [Authorize(PermissionPermissions.Menus.Update)]
    Task UpdateAsync(UpdateMenuInput input);

    /// <summary>
    /// 通过Id获取菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [GetCachingIntercept("id:{0}")]
    Task<GetMenuOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 根据id删除菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetMenuOutput), "id:{0}")]
    [Authorize(PermissionPermissions.Menus.Delete)]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 分页获取菜单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetMenuPageOutput>> GetPageAsync(GetMenuPageInput input);
}