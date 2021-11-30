using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
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

    public async Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync(long id)
    {
        var organizations = await _organizationDomainService.GetTreeAsync(id);
        return organizations.Adapt<ICollection<GetOrganizationTreeOutput>>();
    }
}