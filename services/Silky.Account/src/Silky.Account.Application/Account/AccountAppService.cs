using System.Threading.Tasks;
using Mapster;
using Silky.Account.Application.Contracts.Account;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Identity.Domain;
using Silky.Rpc.Runtime.Server;

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
        return SignInManager.PasswordSignInAsync(input.Account, input.Password, true);
    }

    public async Task<GetCurrentUserInfoOutput> GetCurrentUserInfoAsync()
    {
        var user = await UserManager.GetByIdAsync(long.Parse(_session.UserId.ToString()));
        return user.Adapt<GetCurrentUserInfoOutput>();
    }
}