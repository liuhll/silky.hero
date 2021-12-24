using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Silky.Core.Exceptions;
using Silky.Jwt;
using Silky.Rpc.Security;

namespace Silky.Identity.Domain;

public class IdentitySignInManager
{
    private readonly IUserConfirmation<IdentityUser> _confirmation;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public IdentitySignInManager(
        IdentityUserManager userManager,
        IOptions<IdentityOptions> optionsAccessor,
        IUserConfirmation<IdentityUser> confirmation,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        UserManager = userManager;
        _confirmation = confirmation;
        _jwtTokenGenerator = jwtTokenGenerator;
        Options = optionsAccessor?.Value ?? new IdentityOptions();
        Logger = NullLogger<IdentitySignInManager>.Instance;
    }

    protected IdentityUserManager UserManager { get; }

    protected IdentityOptions Options { get; set; }


    public ILogger<IdentitySignInManager> Logger { get; set; }

    public async Task<string> PasswordSignInAsync(string account, string password, long? tenantId,
        bool lockoutOnFailure)
    {
        var user = await UserManager.FindByAccountAsync(account, tenantId, true);
        if (user == null)
        {
            throw new UserFriendlyException($"不存在账号为{account}的用户");
        }

        await PreSignInCheck(user);
        if (!await UserManager.CheckPasswordAsync(user, password))
        {
            Logger.LogWarning(2, "User {userId} failed to provide the correct password.",
                await UserManager.GetUserIdAsync(user));
            if (UserManager.SupportsUserLockout && lockoutOnFailure)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await UserManager.AccessFailedAsync(user);
            }

            throw new AuthenticationException("密码不正确");
        }

        var alwaysLockout =
            AppContext.TryGetSwitch("Microsoft.AspNetCore.Identity.CheckPasswordSignInAlwaysResetLockoutOnSuccess",
                out var enabled) && enabled;
        if (alwaysLockout)
        {
            await ResetLockout(user);
        }

        var payload = GetUserPayloadAsync(user);
        return _jwtTokenGenerator.Generate(payload);
    }

    private IDictionary<string, object> GetUserPayloadAsync(IdentityUser user)
    {
        var payload = new Dictionary<string, object>()
        {
            { ClaimTypes.NameIdentifier, user.Id },
            { ClaimTypes.Name, user.UserName },
            { ClaimTypes.Email, user.Email },
            { ClaimTypes.MobilePhone, user.MobilePhone },
        };
        if (user.TenantId.HasValue)
        {
            payload[SilkyClaimTypes.TenantId] = user.TenantId;
        }

        if (user.Claims != null && user.Claims.Any())
        {
            foreach (var userClaim in user.Claims)
            {
                if (!payload.ContainsKey(userClaim.ClaimType))
                {
                    payload.Add(userClaim.ClaimType, userClaim.ClaimValue);
                }
            }
        }

        return payload;
    }

    private async Task PreSignInCheck(IdentityUser user)
    {
        await CanSignInAsync(user);
        if (await IsLockedOut(user))
        {
            throw new AuthenticationException("账号被锁定,暂时无法登录");
        }
    }

    private async Task<bool> IsLockedOut(IdentityUser user)
    {
        return UserManager.SupportsUserLockout && await UserManager.IsLockedOutAsync(user);
    }

    private Task ResetLockout(IdentityUser user)
    {
        if (UserManager.SupportsUserLockout)
        {
            return UserManager.ResetAccessFailedCountAsync(user);
        }

        return Task.CompletedTask;
    }


    private async Task CanSignInAsync(IdentityUser user)
    {
        if (!user.IsActive)
        {
            throw new UserFriendlyException($"账号未被激活");
        }

        if (Options.SignIn.RequireConfirmedPhoneNumber && !await UserManager.IsPhoneNumberConfirmedAsync(user))
        {
            throw new AuthenticationException("不能使用未确认的手机号码登录");
        }

        if (Options.SignIn.RequireConfirmedEmail && !await UserManager.IsEmailConfirmedAsync(user))
        {
            throw new AuthenticationException("不能使用未确认的电子邮件登录");
        }

        if (Options.SignIn.RequireConfirmedAccount && !await _confirmation.IsConfirmedAsync(UserManager, user))
        {
            throw new AuthenticationException("不能使用未确认的账号登录");
        }
    }
}