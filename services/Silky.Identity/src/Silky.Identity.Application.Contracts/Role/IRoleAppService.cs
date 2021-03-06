using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Hero.Common.Enums;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Domain.Shared;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;
using Silky.Transaction;

namespace Silky.Identity.Application.Contracts.Role;

/// <summary>
/// 角色信息服务
/// </summary>
[ServiceRoute]
[Authorize]
public interface IRoleAppService
{
    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.Create)]
    [RemoveCachingIntercept(typeof(ICollection<GetRoleOutput>),"allocationOrganizationRoleList")]
    [RemoveCachingIntercept(typeof(ICollection<GetRoleOutput>),"getPublicRoleList")]
    Task CreateAsync(CreateRoleInput input);
    
    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetRoleOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(GetRoleDetailOutput), "id:detail:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<string>),"permissions:roleId:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetRoleOutput>),"allocationOrganizationRoleList")]
    [RemoveCachingIntercept(typeof(ICollection<GetRoleOutput>),"getPublicRoleList")]
    [Authorize(IdentityPermissions.Roles.Update)]
    Task UpdateAsync(UpdateRoleInput input);

    /// <summary>
    /// 根据id获取角色详情信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}/detail")]
    [GetCachingIntercept("id:detail:{0}")]
    [Authorize(IdentityPermissions.Roles.LookDetail)]
    Task<GetRoleDetailOutput> GetDetailAsync([CacheKey(0)] long id);
    
    /// <summary>
    /// 根据id获取角色基础信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [GetCachingIntercept("id:{0}")]
    Task<GetRoleOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 根据Id删除角色信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetRoleOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(GetRoleDetailOutput), "id:detail:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<string>),"permissions:roleId:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetRoleOutput>),"allocationOrganizationRoleList")]
    [RemoveCachingIntercept(typeof(ICollection<GetRoleOutput>),"getPublicRoleList")]
    [Authorize(IdentityPermissions.Roles.Delete)]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 检查角色是否存在
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("check")]
    Task<bool> CheckAsync(CheckRoleInput input);

    /// <summary>
    /// 授权角色菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.SetMenus)]
    [HttpPut("menus")]
    [RemoveCachingIntercept(typeof(ICollection<string>),"permissions:roleId:{0}")]
    [RemoveCachingIntercept(typeof(GetRoleOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(GetRoleDetailOutput), "id:detail:{0}")]
    [Governance(TimeoutMillSeconds = 10000)]
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
    [RemoveCachingIntercept(typeof(GetRoleOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(GetRoleDetailOutput), "id:detail:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<string>),"permissions:roleId:{0}")]
    Task SetDataRangeAsync(UpdateRoleDataRangeInput input);
    
    /// <summary>
    /// 通过Id获取数据权限范围
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Roles.SetDataRange)]
    [HttpGet("datarange/{id:long}")]
    Task<GetRoleDataRangeOutput> GetDataRangeAsync(long id);

    Task<ICollection<GetRoleOutput>> GetListAsync([FromQuery]string realName, [FromQuery] string name,[FromQuery] Status? status);

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

    [ProhibitExtranet]
    [Transaction]
    Task<string> CreateSuperRoleAsync(long tenantId, string superRoleName, string superRealName);

    [GetCachingIntercept("allocationOrganizationRoleList")]
    [ProhibitExtranet]
    Task<ICollection<GetRoleOutput>> GetAllocationOrganizationRoleListAsync();

    [GetCachingIntercept("getPublicRoleList")] 
    [ProhibitExtranet]
    Task<ICollection<GetRoleOutput>> GetPublicRoleListAsync();
    
}