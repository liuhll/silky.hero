using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Silky.Account.Application.Contracts.Account;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Rpc;
using Silky.Core.Runtime.Session;
using Silky.Identity.Domain;
using Silky.Rpc.Configuration;

namespace Silky.Account.Application.Account;

public class AccountAppService : IAccountAppService
{
    protected IdentitySignInManager SignInManager { get; }
    protected IdentityUserManager UserManager { get; }

    private ISession _session;

    public AccountAppService(IdentitySignInManager signInManager,
        IdentityUserManager userManager)
    {
        SignInManager = signInManager;
        UserManager = userManager;
        _session = NullSession.Instance;
    }


    public Task<string> LoginAsync(LoginInput input)
    {
        return SignInManager.PasswordSignInAsync(input.Account, input.Password, input.TenantName, true);
    }

    public async Task<int> CheckUrl()
    {
        if (!_session.IsLogin())
        {
            return 401;
        }

        return 200;
    }

    public async Task<int> SubmitUrl(SpecificationWithTenantAuth authInfo)
    {
        var loginInput = authInfo.Adapt<LoginInput>();
        var token = await LoginAsync(loginInput);
        RpcContext.Context.SigninToSwagger(token);
        return 200;
    }

    public async Task<GetCurrentUserInfoOutput> GetCurrentUserInfoAsync()
    {
        var user = await UserManager.GetByIdAsync(long.Parse(_session.UserId.ToString()));
        var currentUser = user.Adapt<GetCurrentUserInfoOutput>();
        currentUser.Roles = await UserManager.GetRolesAsync(user);
        currentUser.DataRange = (await GetCurrentUserDataRangeAsync()).Adapt<GetCurrentUserDataRangeOutput>();
        return currentUser;
    }

    public async Task<GetCurrentUserDataRange> GetCurrentUserDataRangeAsync()
    {
        return (await UserManager.GetUserDataRange(long.Parse(_session.UserId.ToString())))
            .Adapt<GetCurrentUserDataRange>();
    }

    public Task<ICollection<GetCurrentUserMenuOutput>> GetCurrentUserMenusAsync()
    {
        return UserManager.GetUserMenusAsync(_session.UserId.To<long>());
    }

    public Task<string[]> GetCurrentUserPermissionCodesAsync()
    {
        return UserManager.GetUserPermissionCodesAsync(_session.UserId.To<long>());
    }
}