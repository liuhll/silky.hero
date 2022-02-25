using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Application.Contracts.User;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Organization.Domain;
using Silky.Position.Application.Contracts.Position;
using Silky.Transaction.Tcc;

namespace Silky.Organization.Application.Organization;

public class OrganizationAppService : IOrganizationAppService
{
    private readonly IOrganizationDomainService _organizationDomainService;
    private readonly IUserAppService _userAppService;
    private readonly IRoleAppService _roleAppService;
    private readonly IPositionAppService _positionAppService;
    private readonly IRepository<OrganizationRole> _organizationRoleRepository;

    public OrganizationAppService(
        IOrganizationDomainService organizationDomainService,
        IUserAppService userAppService,
        IRoleAppService roleAppService,
        IPositionAppService positionAppService,
        IRepository<OrganizationRole> organizationRoleRepository)
    {
        _organizationDomainService = organizationDomainService;
        _userAppService = userAppService;
        _roleAppService = roleAppService;
        _organizationRoleRepository = organizationRoleRepository;
        _positionAppService = positionAppService;
    }

    public Task CreateAsync(CreateOrganizationInput input)
    {
        return _organizationDomainService.CreateAsync(input);
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
            .FirstOrDefaultAsync(p => p.Id == id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的组织机构");
        }

        var publicRoles = await _roleAppService.GetPublicRoleListAsync();
        var organizationOutput = organization.Adapt<GetOrganizationOutput>();
        await organizationOutput.SetRoleInfo(publicRoles);
        return organizationOutput;
    }

    // public async Task<PagedList<GetOrganizationPageOutput>> GetPageAsync(GetOrganizationPageInput input)
    // {
    //     var organizations = await _organizationDomainService.OrganizationRepository
    //         .Where(input.Id.HasValue, o => o.Id == input.Id || o.ParentId == input.Id)
    //         .Where(!input.Name.IsNullOrEmpty(), o => o.Name.Contains(input.Name))
    //         .Where(input.Status.HasValue, o => o.Status == input.Status)
    //         .OrderByDescending(p => p.Sort)
    //         .ProjectToType<GetOrganizationPageOutput>()
    //         .ToPagedListAsync(input.PageIndex, input.PageSize);
    //     return organizations;
    // }

    public async Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync()
    {
        var organizations = await _organizationDomainService.GetTreeAsync();
        return organizations;
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
    
    public async Task<long[]> GetOrganizationRoleIdsAsync(long[] organizationIds)
    {
        return await _organizationRoleRepository
            .AsQueryable(false)
            .Where(p => organizationIds.Contains(p.OrganizationId))
            .Select(p => p.RoleId)
            .ToArrayAsync();
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