using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Rpc.Routing;

namespace Silky.Position.Application.Contracts.Position;

[ServiceRoute]
public interface IPositionAppService
{
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateAsync(CreateOrUpdatePositionInput input);
}