using System.Threading.Tasks;
using Mapster;
using Silky.Core.Exceptions;
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

    public async Task<GetPositionOutput> GetAsync(long id)
    {
        var position = await _positionDomainService.PositionRepository.FindOrDefaultAsync(id);
        if (position == null)
        {
            throw new UserFriendlyException($"不存在id为{id}的职位信息");
        }

        return position.Adapt<GetPositionOutput>();
    }
}