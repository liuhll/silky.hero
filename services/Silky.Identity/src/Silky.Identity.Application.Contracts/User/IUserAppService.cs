using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

namespace Silky.Identity.Application.Contracts.User;

/// <summary>
/// 用户信息服务
/// </summary>
[ServiceRoute]
public interface IUserAppService
{
    /// <summary>
    /// 新增/更新用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    [RemoveCachingIntercept(typeof(GetUserOutput), "id:{0}")]
    Task CreateOrUpdateAsync(CreateOrUpdateUserInput input);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetUserOutput), "id:{0}")]
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

    /// <summary>
    /// 更新用户声明
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/claims")]
    Task UpdateClaimTypesAsync(long userId, ICollection<UpdateClaimTypeInput> inputs);

    /// <summary>
    /// 根据id锁定用户账号
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="lockoutSeconds"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/lock/{lockoutSeconds:int}")]
    Task LockAsync(long userId, int lockoutSeconds);

    /// <summary>
    /// 根据Id解锁用户账号
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/unlock")]
    Task UnLockAsync(long userId);

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("{userId:long}/password")]
    Task ChangePasswordAsync(long userId, ChangePasswordInput input);

    /// <summary>
    /// 获取指定组织机构的用户
    /// </summary>
    /// <param name="organizationId">组织机构id</param>
    /// <returns></returns>
    [Governance(ProhibitExtranet = true)]
    Task<ICollection<GetUserOutput>> GetOrganizationUsersAsync(long organizationId);

    /// <summary>
    /// 判断是否组织机构存在用户
    /// </summary>
    /// <param name="organizationId">组织机构id</param>
    /// <returns></returns>
    [Governance(ProhibitExtranet = true)]
    Task<bool> HasOrganizationUsersAsync(long organizationId);

    /// <summary>
    /// 判断某个职位是否存在用户
    /// </summary>
    /// <param name="positionId">岗位id</param>
    /// <returns></returns>
    [Governance(ProhibitExtranet = true)]
    Task<bool> HasPositionUsersAsync(long positionId);
}