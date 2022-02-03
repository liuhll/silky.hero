using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Domain.Shared;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.Identity.Application.Contracts.Role;

/// <summary>
/// 角色信息服务
/// </summary>
[ServiceRoute]
[Authorize(IdentityPermissions.Roles.Default)]
public interface IRoleAppService
{
    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.Create)]
    Task CreateAsync(CreateRoleInput input);
    
    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetRoleOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(GetCurrentUserDataRange),"CurrentUserDataRange:userId:*")]
    [RemoveCachingIntercept(typeof(ICollection<string>),"permissions:roleId:{0}")]
    [Authorize(IdentityPermissions.Roles.Update)]
    Task UpdateAsync(UpdateRoleInput input);

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
    [RemoveCachingIntercept(typeof(GetCurrentUserDataRange),"CurrentUserDataRange:userId:*")]
    [RemoveCachingIntercept(typeof(ICollection<string>),"permissions:roleId:{0}")]
    [Authorize(IdentityPermissions.Roles.Delete)]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 授权角色菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.SetMenus)]
    [HttpPut("menus")]
    Task SetMenusAsync(UpdateRoleMenuInput input);
    
    /// <summary>
    /// 通过id获取角色菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.SetMenus)]
    [HttpGet("menus/{id:long}")]
    Task<GetRoleMenuOutput> GetMenusAsync(long id);

    /// <summary>
    /// 授权数据权限访问
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.SetDataRange)]
    [HttpPut("datarange")]
    [RemoveCachingIntercept(typeof(GetCurrentUserDataRange),"CurrentUserDataRange:*")]
    Task SetDataRangeAsync(UpdateRoleDataRangeInput input);
    
    /// <summary>
    /// 通过Id获取数据权限范围
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.SetDataRange)]
    [HttpGet("datarange/{id:long}")]
    Task<GetRoleDataRangeOutput> GetDataRangeAsync(long id);

    /// <summary>
    /// 分页查询角色信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetRolePageOutput>> GetPageAsync(GetRolePageInput input);

    [ProhibitExtranet]
    [GetCachingIntercept("permissions:roleId:{0}")]
    Task<ICollection<string>> GetPermissionsAsync([CacheKey(0)]long roleId);

    [ProhibitExtranet]
    Task<bool> CheckHasMenusAsync(long[] menuIds);
}