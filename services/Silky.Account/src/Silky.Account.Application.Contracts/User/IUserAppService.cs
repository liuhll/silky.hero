using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Silky.Account.Application.Contracts.User.Dtos;
using Silky.Rpc.Routing;

namespace Silky.Account.Application.Contracts.User;

[ServiceRoute]
public interface IUserAppService
{
    [AllowAnonymous]
    Task Create(CreateUserInput input);
}