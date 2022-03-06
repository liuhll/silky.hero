using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Exceptions;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.Session;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Application.Contracts.User;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Organization.Domain;
using Silky.Position.Application.Contracts.Position;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Transaction.Tcc;

namespace Silky.Organization.Application.Organization;

public class OrganizationAppService : IOrganizationAppService
{
    private readonly IOrganizationDomainService _organizationDomainService;
    private readonly IUserAppService _userAppService;
    private readonly IRoleAppService _roleAppService;
    private readonly IPositionAppService _positionAppService;
    private readonly IRepository<OrganizationRole> _organizationRoleRepository;
    private readonly IRepository<OrganizationPosition> _organizationPositionRepository;
    private readonly ISession _session;

    public OrganizationAppService(
        IOrganizationDomainService organizationDomainService,
        IUserAppService userAppService,
        IRoleAppService roleAppService,
        IPositionAppService positionAppService,
        IRepository<OrganizationRole> organizationRoleRepository,
        IRepository<OrganizationPosition> organizationPositionRepository)
    {
        _organizationDomainService = organizationDomainService;
        _userAppService = userAppService;
        _roleAppService = roleAppService;
        _organizationRoleRepository = organizationRoleRepository;
        _organizationPositionRepository = organizationPositionRepository;
        _positionAppService = positionAppService;
        _session = NullSession.Instance;
    }

    public Task CreateAsync(CreateOrganizationInput input)
    {
        return _organizationDomainService.CreateAsync(input);
    }

    public Task<bool> CheckAsync(CheckOrganizationInput input)
    {
        return _organizationDomainService
            .OrganizationRepository
            .AnyAsync(p =>
                p.ParentId == input.ParentId && p.Name == input.Name && p.Id != input.Id);
    }

    public async Task<bool> CheckHasDataRangeAsync(long organizationId)
    {
        var currentUserDataRange = await _session.GetCurrentUserDataRangeAsync();
        return currentUserDataRange.IsAllData || currentUserDataRange.OrganizationIds.Any(p => p == organizationId);
    }

    public Task UpdateAsync(UpdateOrganizationInput input)
    {
        return _organizationDomainService.UpdateAsync(input);
    }

    [TccTransaction(ConfirmMethod = "DeleteConfirmAsync", CancelMethod = "DeleteCancelAsync")]
    public Task DeleteAsync(long id)
    {
        return _organizationDomainService.DeleteTryAsync(id);
    }

    public async Task DeleteCancelAsync(long id)
    {
    }

    public Task DeleteConfirmAsync(long id)
    {
        return _organizationDomainService.DeleteConfirmAsync(id);
    }

    public async Task<GetOrganizationOutput> GetAsync(long id)
    {
        var organization = await _organizationDomainService
            .OrganizationRepository
            .AsQueryable(false)
            .Include(p => p.OrganizationRoles)
            .Include(p => p.OrganizationPositions)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的组织机构");
        }

        var publicRoles = await _roleAppService.GetPublicRoleListAsync();
        var publicPositions = await _positionAppService.GetPublicPositionListAsync();
        var organizationOutput = organization.Adapt<GetOrganizationOutput>();
        await organizationOutput.SetRolesInfo(publicRoles);
        await organizationOutput.SetPositionsInfo(publicPositions);
        await organizationOutput.SetIsBelong(_session);
        return organizationOutput;
    }

    public async Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync()
    {
        var organizations = await _organizationDomainService.GetTreeAsync();
        return organizations;
    }

    public Task<bool> CheckHasLeaderAsync(long organizationId)
    {
        return _userAppService.CheckHasLeaderAsync(organizationId);
    }

    public async Task<bool> HasOrganizationAsync(long organizationId)
    {
        return await _organizationDomainService.OrganizationRepository.FindOrDefaultAsync(organizationId) != null;
    }

    public async Task<ICollection<long>> GetSelfAndChildrenOrganizationIdsAsync(long organizationId)
    {
        var childrenOrganizations =
            await _organizationDomainService.GetChildrenOrganizationsAsync(organizationId);
        return childrenOrganizations.Select(p => p.Id).ToList();
    }

    public Task<ICollection<GetRoleOutput>> GetAllocationRoleListAsync()
    {
        return _roleAppService.GetAllocationOrganizationRoleListAsync();
    }

    public Task<ICollection<GetPositionOutput>> GetAllocationPositionListAsync()
    {
        return _positionAppService.GetAllocationOrganizationPositionListAsync();
    }

    public async Task<long[]> GetOrganizationRoleIdsAsync(long[] organizationIds)
    {
        return await _organizationRoleRepository
            .AsQueryable(false)
            .Where(p => organizationIds.Contains(p.OrganizationId))
            .Select(p => p.RoleId)
            .ToArrayAsync();
    }

    public async Task<long[]> GetOrganizationPositionIdsAsync(long organizationId)
    {
        var setPositionIds = await _organizationPositionRepository
            .AsQueryable(false)
            .Where(p => p.OrganizationId == organizationId)
            .Select(p => p.PositionId)
            .ToArrayAsync();

        var publicPositionIds = (await _positionAppService.GetPublicPositionListAsync())
            .Select(p => p.Id);
        return setPositionIds.Union(publicPositionIds).ToArray();
    }

    public async Task<ICollection<GetOrganizationOutput>> GetCurrentOrganizationListAsync()
    {
        var currentUserDataRange = await _session.GetCurrentUserDataRangeAsync();
        return await _organizationPositionRepository
            .AsQueryable(false)
            .Where(!currentUserDataRange.IsAllData,
                p => currentUserDataRange.OrganizationIds.Contains(p.OrganizationId))
            .ProjectToType<GetOrganizationOutput>()
            .ToListAsync();
    }

    public Task SetAllocationRoleListAsync(long id, long[] roleIds)
    {
        return _organizationDomainService.SetAllocationRoleListAsync(id, roleIds);
    }

    public Task SetAllocationPositionListAsync(long id, long[] positionIds)
    {
        return _organizationDomainService.SetAllocationPositionListAsync(id, positionIds);
    }

    public Task<PagedList<GetOrganizationUserPageOutput>> GetUserPageAsync(long id, GetOrganizationUserPageInput input)
    {
        return _userAppService.GetOrganizationUserPageAsync(id, input);
    }

    public async Task<ICollection<long>> GetUserIdsAsync(long id)
    {
        return await _userAppService.GetUserIdsAsync(id);
    }

    public async Task AddUsers(long id, ICollection<AddOrganizationUserInput> inputs)
    {
        var organization = await _organizationDomainService.OrganizationRepository.FindOrDefaultAsync(id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的组织机构");
        }

        await _userAppService.AddOrganizationUsers(id, inputs);
    }

    public async Task RemoveUsers(long id, long[] userIds)
    {
        var organization = await _organizationDomainService.OrganizationRepository.FindOrDefaultAsync(id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的组织机构");
        }

        await _userAppService.RemoveOrganizationUsers(id, userIds);
    }
}