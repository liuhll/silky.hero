using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Rpc.Configuration;
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
    
    [HttpPost, UnWrapperResult,AllowAnonymous]
    Task<int> CheckUrl();

    [HttpPost, UnWrapperResult,AllowAnonymous]
    Task<int> SubmitUrl([FromForm]SpecificationWithTenantAuth authInfo);

    /// <summary>
    /// 获取当前登录用户信息
    /// </summary>
    /// <returns></returns>
    [GetCachingIntercept("CurrentUserInfo", OnlyCurrentUserData = true)]
    [Authorize]
    Task<GetCurrentUserInfoOutput> GetCurrentUserInfoAsync();

    /// <summary>
    /// 获取当前登录用户的菜单权限
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("menus")]
    [GetCachingIntercept("CurrentUserMenus", OnlyCurrentUserData = true)]
    Task<ICollection<GetCurrentUserMenuOutput>> GetCurrentUserMenusAsync();

    /// <summary>
    /// 获取当前登录用户所有的权限码
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("permissioncodes")]
    [GetCachingIntercept("CurrentUserPermissionCodes", OnlyCurrentUserData = true)]
    Task<string[]> GetCurrentUserPermissionCodesAsync();
    
    [Authorize]
    [GetCachingIntercept("CurrentUserDataRange", OnlyCurrentUserData = true)]
    [ProhibitExtranet]
    Task<GetCurrentUserDataRange> GetCurrentUserDataRangeAsync();
}