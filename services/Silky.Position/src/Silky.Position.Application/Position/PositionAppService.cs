using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.Hero.Common.Enums;
using Silky.Position.Application.Contracts.Position;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Position.Domain;

namespace Silky.Position.Application.Position;

public class PositionAppService : IPositionAppService
{
    private readonly IPositionDomainService _positionDomainService;

    public PositionAppService(IPositionDomainService positionDomainService)
    {
        _positionDomainService = positionDomainService;
    }

    public Task CreateAsync(CreatePositionInput input)
    {
        return _positionDomainService.CreateAsync(input);
    }

    public Task UpdateAsync(UpdatePositionInput input)
    {
        return _positionDomainService.UpdateAsync(input);
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

    public Task DeleteAsync(long id)
    {
        return _positionDomainService.DeleteAsync(id);
    }

    public Task<PagedList<GetPositionPageOutput>> GetPageAsync(GetPositionPageInput input)
    {
        return _positionDomainService.PositionRepository
            .AsQueryable(false)
            .Where(!input.Name.IsNullOrEmpty(), p => p.Name.Contains(input.Name))
            .Where(input.Status.HasValue, p => p.Status == input.Status)
            .OrderByDescending(p=> p.Sort)
            .ProjectToType<GetPositionPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
    }

    public async Task<bool> HasPositionAsync(long positionId)
    {
        return await _positionDomainService.PositionRepository.FindOrDefaultAsync(positionId) != null;
    }

    public async Task<ICollection<GetPositionOutput>> GetListAsync(string name)
    {
        return await _positionDomainService.PositionRepository
            .AsQueryable(false)
            .Where(!name.IsNullOrEmpty(), p => p.Name.Contains(name))
            //.Where(p => p.Status == Status.Valid)
            .ProjectToType<GetPositionOutput>()
            .ToListAsync();
    }
}