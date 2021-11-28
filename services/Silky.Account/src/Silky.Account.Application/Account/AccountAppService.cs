using System.Threading.Tasks;
using Silky.Account.Application.Contracts.Account;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Identity.Domain;

namespace Silky.Account.Application.Account;

public class AccountAppService : IAccountAppService
{
    protected IdentitySignInManager SignInManager { get; }

    public AccountAppService(IdentitySignInManager signInManager)
    {
        SignInManager = signInManager;
    }


    public Task<string> LoginAsync(LoginInput input)
    {
        return SignInManager.PasswordSignInAsync(input.Account, input.Password, true);
    }
}