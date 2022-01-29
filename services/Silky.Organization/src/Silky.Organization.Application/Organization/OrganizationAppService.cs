using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.Identity.Application.Contracts.User;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Organization.Domain;

namespace Silky.Organization.Application.Organization;

public class OrganizationAppService : IOrganizationAppService
{
    private readonly IOrganizationDomainService _organizationDomainService;
    private readonly IUserAppService _userAppService;

    public OrganizationAppService(IOrganizationDomainService organizationDomainService,
        IUserAppService userAppService)
    {
        _organizationDomainService = organizationDomainService;
        _userAppService = userAppService;
    }

    public Task CreateAsync(CreateOrganizationInput input)
    {
        return _organizationDomainService.CreateAsync(input);
    }

    public Task UpdateAsync(UpdateOrganizationInput input)
    {
        return _organizationDomainService.UpdateAsync(input);
    }

    public Task DeleteAsync(long id)
    {
        return _organizationDomainService.DeleteAsync(id);
    }

    public async Task<GetOrganizationOutput> GetAsync(long id)
    {
        var organization = await _organizationDomainService.OrganizationRepository.FindOrDefaultAsync(id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的组织机构");
        }

        return organization.Adapt<GetOrganizationOutput>();
    }

    public async Task<PagedList<GetOrganizationPageOutput>> GetPageAsync(GetOrganizationPageInput input)
    {
        var organizations = await _organizationDomainService.OrganizationRepository
            .Where(input.Id.HasValue, o => o.Id == input.Id || o.ParentId == input.Id)
            .Where(!input.Name.IsNullOrEmpty(), o => o.Name.Contains(input.Name))
            .Where(input.Status.HasValue, o => o.Status == input.Status)
            .OrderByDescending(p => p.Sort)
            .ProjectToType<GetOrganizationPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return organizations;
    }

    public async Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync()
    {
        var organizations = await _organizationDomainService.GetTreeAsync();
        return organizations;
    }

    public async Task<bool> HasOrganizationAsync(long organizationId)
    {
        return await _organizationDomainService.OrganizationRepository.FindOrDefaultAsync(organizationId) != null;
    }

    public async Task<IEnumerable<long>> GetSelfAndChildrenOrganizationIdsAsync(long organizationId)
    {
        var allOrganizations = await _organizationDomainService.OrganizationRepository.AsQueryable(false).ToListAsync();
        var childrenOrganizations =
            await _organizationDomainService.GetChildrenOrganizationsAsync(organizationId);
        return childrenOrganizations.Select(p => p.Id).ToList();
    }

    public Task<PagedList<GetOrganizationUserPageOutput>> GetUserPageAsync(long id, GetOrganizationUserPageInput input)
    {
        return _userAppService.GetOrganizationUserPageAsync(id,input);
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
}