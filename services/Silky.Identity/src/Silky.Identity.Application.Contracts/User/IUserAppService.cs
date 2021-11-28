using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Rpc.Routing;

namespace Silky.Identity.Application.Contracts.User;

[ServiceRoute]
public interface IUserAppService
{
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateAsync(CreateOrUpdateUserInput input);

   // [HttpDelete]
    Task DeleteAsync(long userId);
}