using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Extensions;
using Silky.Hero.Common.Enums;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Position.Application.Contracts.Position;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Position.Domain;

namespace Silky.Position.Application.Position;

public class PositionAppService : IPositionAppService
{
    private readonly IPositionDomainService _positionDomainService;
    private readonly IOrganizationAppService _organizationAppService;
    private readonly ISession _session;

    public PositionAppService(IPositionDomainService positionDomainService,
        IOrganizationAppService organizationAppService)
    {
        _positionDomainService = positionDomainService;
        _organizationAppService = organizationAppService;
        _session = NullSession.Instance;
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
            .OrderByDescending(p => p.Sort)
            .ProjectToType<GetPositionPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
    }

    public async Task<ICollection<GetAllocationOrganizationPositionOutput>> GetPositionListAsync(long organizationId, bool? isAll)
    {
        var organizationPositionIds = await _organizationAppService.GetOrganizationPositionIdsAsync(organizationId);
        var outputList = await _positionDomainService.PositionRepository
            .AsQueryable(false)
            .Where(isAll == false,
                p => (organizationPositionIds.Contains(p.Id) || p.IsPublic) && p.Status == Status.Valid)
            .Where(isAll == true || isAll == null, p=> p.Status == Status.Valid)
            .ProjectToType<GetAllocationOrganizationPositionOutput>()
            .ToListAsync();
        foreach (var output in outputList)
        {
            output.IsBelong = organizationPositionIds.Any(p => p == output.Id);
        }

        return outputList.OrderByDescending(p=> p.IsBelong).ToList();
    }

    public Task<bool> CheckAsync(CheckPositionInput input)
    {
        return _positionDomainService.PositionRepository.AnyAsync(p => p.Name == input.Name && p.Id != input.Id, false);
    }

    public async Task<bool> CheckHasDataRangeAsync(long organizationId, long positionId)
    {
        var organizationPositionIds = await _organizationAppService.GetOrganizationPositionIdsAsync(organizationId);
        return organizationPositionIds.Any(p => p == positionId);
    }

    public async Task<bool> HasPositionAsync(long positionId)
    {
        return await _positionDomainService.PositionRepository.FindOrDefaultAsync(positionId) != null;
    }

    public async Task<ICollection<GetPositionOutput>> GetListAsync(string name, Status? status)
    {
        return await _positionDomainService.PositionRepository
            .AsQueryable(false)
            .Where(!name.IsNullOrEmpty(), p => p.Name.Contains(name))
            .Where(status.HasValue, p => p.Status == status)
            .ProjectToType<GetPositionOutput>()
            .ToListAsync();
    }

    public async Task<ICollection<GetPositionOutput>> GetAllocationOrganizationPositionListAsync()
    {
        return await _positionDomainService
            .PositionRepository
            .AsQueryable(false)
            .ProjectToType<GetPositionOutput>()
            .ToListAsync();
    }

    public async Task<ICollection<GetPositionOutput>> GetPublicPositionListAsync()
    {
        return await _positionDomainService
            .PositionRepository
            .AsQueryable(false)
            .Where(p => p.IsPublic)
            .ProjectToType<GetPositionOutput>()
            .ToListAsync();
    }
}