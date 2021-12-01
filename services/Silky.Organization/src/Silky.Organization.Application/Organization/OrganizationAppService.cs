using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Silky.Core.Exceptions;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Organization.Domain.Organizations;

namespace Silky.Organization.Application.Organization;

public class OrganizationAppService : IOrganizationAppService
{
    private readonly IOrganizationDomainService _organizationDomainService;

    public OrganizationAppService(IOrganizationDomainService organizationDomainService)
    {
        _organizationDomainService = organizationDomainService;
    }

    public Task CreateOrUpdateAsync(CreateOrUpdateOrganizationInput input)
    {
        if (!input.Id.HasValue)
        {
            return _organizationDomainService.CreateAsync(input);
        }

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

    public async Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync()
    {
        var organizations = await _organizationDomainService.GetTreeAsync();
        return organizations;
    }
}