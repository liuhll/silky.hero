using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Rpc.Routing;
using Silky.Rpc.Security;

namespace Silky.Identity.Application.Contracts.User;

[ServiceRoute]
public interface IUserAppService
{
    [AllowAnonymous]
    [HttpPost("create")]
    [HttpPut("update")]
    Task CreateOrUpdate(CreateOrUpdateUserInput input);
}