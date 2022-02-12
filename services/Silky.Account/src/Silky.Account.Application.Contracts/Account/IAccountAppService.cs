using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.Account.Application.Contracts.Account;

/// <summary>
/// 账号服务
/// </summary>
[ServiceRoute]
public interface IAccountAppService
{
    /// <summary>
    /// 登录接口
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    Task<string> LoginAsync(LoginInput input);

    /// <summary>
    /// 获取当前登录用户信息
    /// </summary>
    /// <returns></returns>
    [GetCachingIntercept("CurrentUserInfo", OnlyCurrentUserData = true)]
    [Authorize]
    Task<GetCurrentUserInfoOutput> GetCurrentUserInfoAsync();

    [Authorize]
    [HttpGet("menus")]
    [GetCachingIntercept("CurrentUserMenus", OnlyCurrentUserData = true)]
    Task<ICollection<GetCurrentUserMenuOutput>> GetCurrentUserMenusAsync();

    [Authorize]
    [HttpGet("permissioncodes")]
    [GetCachingIntercept("CurrentUserPermissioncodes", OnlyCurrentUserData = true)]
    Task<string[]> GetCurrentUserPermissionCodesAsync();

    [Authorize]
    [GetCachingIntercept("CurrentUserDataRange", OnlyCurrentUserData = true)]
    [ProhibitExtranet]
    Task<GetCurrentUserDataRange> GetCurrentUserDataRangeAsync();
}