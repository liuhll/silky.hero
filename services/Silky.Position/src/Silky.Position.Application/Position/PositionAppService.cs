using System.Threading.Tasks;
using Silky.Position.Application.Contracts.Position;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Position.Domain.Positions;

namespace Silky.Position.Application.Position;

public class PositionAppService : IPositionAppService
{
    private readonly IPositionDomainService _positionDomainService;

    public PositionAppService(IPositionDomainService positionDomainService)
    {
        _positionDomainService = positionDomainService;
    }

    public Task CreateOrUpdateAsync(CreateOrUpdatePositionInput input)
    {
        if (!input.Id.HasValue)
        {
            return _positionDomainService.CreateAsync(input);
        }
        else
        {
            return _positionDomainService.UpdateAsync(input);
        }
    }
}