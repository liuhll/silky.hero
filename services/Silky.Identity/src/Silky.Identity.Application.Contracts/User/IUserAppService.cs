using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Identity.Domain.Shared;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.Identity.Application.Contracts.User;

/// <summary>
/// 用户信息服务
/// </summary>
[ServiceRoute]
[Authorize]
public interface IUserAppService
{
    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(IdentityPermissions.Users.Create)]
    Task CreateAsync(CreateUserInput input);

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetUserOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(GetUserRoleOutput),"roles:userId:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<long>),"roleIds:userId:{0}")]
    [Authorize(IdentityPermissions.Users.Update)]
    Task UpdateAsync(UpdateUserInput input);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetUserOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(GetUserRoleOutput),"roles:userId:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<long>),"roleIds:userId:{0}")]
    [Authorize(IdentityPermissions.Users.Delete)]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 根据id获取用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetUserOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 分页查询用户信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetUserPageOutput>> GetPageAsync(GetUserPageInput input);

    [HttpGet("{organizationId:long}/organizationuser/page")]
    Task<PagedList<GetAddOrganizationUserOutput>> GetAddOrganizationUserPageAsync(long organizationId, GetAddOrganizationUserPageInput input);

    /// <summary>
    /// 更新用户声明
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/claims")]
    [Authorize(IdentityPermissions.Users.UpdateClaimTypes)]
    Task UpdateClaimTypesAsync(long userId, ICollection<UpdateClaimTypeInput> inputs);

    /// <summary>
    /// 授权用户角色
    /// </summary>
    /// <param name="userId">用户Id</param>
    /// <param name="roleNames">角色名称</param>
    /// <returns></returns>
    [HttpPut("{userId:long}/roles")]
    [Authorize(IdentityPermissions.Users.SetRoles)]
    [RemoveCachingIntercept(typeof(GetUserRoleOutput),"roles:userId:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<long>),"roleIds:userId:{0}")]
    Task SetRolesAsync([CacheKey(0)]long userId, ICollection<string> roleNames);

    /// <summary>
    /// 通过用户Id获取角色名称
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId:long}/roles")]
    [Authorize(IdentityPermissions.Users.SetRoles)]
    [GetCachingIntercept("roles:userId:{0}")]
    Task<GetUserRoleOutput> GetRolesAsync([CacheKey(0)]long userId);

    /// <summary>
    /// 根据id锁定用户账号
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="lockoutSeconds"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/lock/{lockoutSeconds:int}")]
    [Authorize(IdentityPermissions.Users.Lock)]
    Task LockAsync(long userId, int lockoutSeconds);
    
    /// <summary>
    /// 根据Id解锁用户账号
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/unlock")]
    [Authorize(IdentityPermissions.Users.UnLock)]
    Task UnLockAsync(long userId);
    
    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/password")]
    [Authorize(IdentityPermissions.Users.ChangePassword)]
    Task ChangePasswordAsync(long userId, ChangePasswordInput input);

    //[HttpGet("{userId:long}/{organizationId:long}/position")]
    //Task<GetUserPositionOutput> GetUserPositionInfo(long userId, long organizationId);

    /// <summary>
    /// 获取指定组织机构的用户
    /// </summary>
    /// <param name="organizationId">组织机构id</param>
    /// <returns></returns>
    [ProhibitExtranet]
    Task<ICollection<GetUserOutput>> GetOrganizationUsersAsync(long organizationId);

    /// <summary>
    /// 判断是否组织机构存在用户
    /// </summary>
    /// <param name="organizationId">组织机构id</param>
    /// <returns></returns>
    [ProhibitExtranet]
    Task<bool> HasOrganizationUsersAsync(long organizationId);

    /// <summary>
    /// 判断某个职位是否存在用户
    /// </summary>
    /// <param name="positionId">岗位id</param>
    /// <returns></returns>
    [ProhibitExtranet]
    Task<bool> HasPositionUsersAsync(long positionId);

    /// <summary>
    /// 通过用户Id获取角色Ids
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [ProhibitExtranet]
    [GetCachingIntercept("roleIds:userId:{0}")]
    Task<ICollection<long>> GetRoleIdsAsync([CacheKey(0)]long userId);

    [ProhibitExtranet]
    Task<ICollection<long>> GetUserIdsAsync(long organizationId);

    [ProhibitExtranet]
    Task AddOrganizationUsers(long organizationId, ICollection<AddOrganizationUserInput> inputs);

    
}