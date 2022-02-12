using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Permission.Domain.Shared;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.Permission.Application.Contracts.Menu;

/// <summary>
/// 菜单服务
/// </summary>
[ServiceRoute]
[Authorize]
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
    [RemoveCachingIntercept(typeof(bool), "HasMenu:{0}")]
    [Authorize(PermissionPermissions.Menus.Update)]
    Task UpdateAsync(UpdateMenuInput input);

    /// <summary>
    /// 通过Id获取菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [GetCachingIntercept("id:{0}")]
    [Authorize(PermissionPermissions.Menus.LookDetail)]
    Task<GetMenuOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 根据id删除菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetMenuOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(bool), "HasMenu:{0}")]
    [Authorize(PermissionPermissions.Menus.Delete)]
    Task DeleteAsync([CacheKey(0)] long id);

    
    /// <summary>
    /// 获取菜单树
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<ICollection<GetMenuTreeOutput>> GetTreeAsync([FromQuery]string name);

    /// <summary>
    /// 根据Id判断是否存在菜单
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    [GetCachingIntercept("HasMenu:{0}")]
    [ProhibitExtranet]
    Task<bool> HasMenuAsync([CacheKey(0)]long menuId);

    [ProhibitExtranet]
    Task<ICollection<string>> GetPermissions(List<long> menuIds);

    [ProhibitExtranet]
    Task<ICollection<GetMenuOutput>> GetMenusAsync(long[] menuIds,bool includeParents = true);
}