using System.Threading.Tasks;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Security;

namespace Silky.Account.Application.Contracts.Account;

[ServiceRoute]
public interface IAccountAppService
{
    [AllowAnonymous]
    Task<string> LoginAsync(LoginInput input);

     [GetCachingIntercept("CurrentUserInfo", OnlyCurrentUserData = true)]
    Task<GetCurrentUserInfoOutput> GetCurrentUserInfoAsync();
}