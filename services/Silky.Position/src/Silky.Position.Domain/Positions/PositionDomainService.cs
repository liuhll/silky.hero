using System.Threading.Tasks;
using Mapster;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Position.Application.Contracts.Position.Dtos;

namespace Silky.Position.Domain.Positions;

public class PositionDomainService : IPositionDomainService
{
    public PositionDomainService(IRepository<Position> positionRepository)
    {
        PositionRepository = positionRepository;
    }

    public IRepository<Position> PositionRepository { get; }

    public async Task CreateAsync(CreateOrUpdatePositionInput input)
    {
        var exsitPosition = await PositionRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (exsitPosition != null)
        {
            throw new UserFriendlyException($"已经存在名称为{input.Name}的职位");
        }

        exsitPosition = await PositionRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
        if (exsitPosition != null)
        {
            throw new UserFriendlyException($"已经存在编码为{input.Code}的职位");
        }

        var position = input.Adapt<Position>();
        await PositionRepository.InsertAsync(position);
    }

    public async Task UpdateAsync(CreateOrUpdatePositionInput input)
    {
        Check.NotNull(input.Id, nameof(input.Id));
        var position = await PositionRepository.FindOrDefaultAsync(input.Id.Value);
        if (position == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}的职位");
        }

        if (!position.Name.Equals(input.Name))
        {
            var exsitPosition = await PositionRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (exsitPosition != null)
            {
                throw new UserFriendlyException($"已经存在名称为{input.Name}的职位");
            }
        }

        if (!position.Code.Equals(input.Code))
        {
            var exsitPosition = await PositionRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
            if (exsitPosition != null)
            {
                throw new UserFriendlyException($"已经存在编码为{input.Code}的职位");
            }
        }

        position = input.Adapt(position);
        await PositionRepository.UpdateAsync(position);
    }
}