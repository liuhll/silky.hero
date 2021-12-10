using System.Threading.Tasks;
using Silky.Tenant.Application.Contracts.Tenant;
using Silky.Tenant.Application.Contracts.Tenant.Dtos;
using Silky.Tenant.Domain;

namespace Silky.Tenant.Application.Tenant;

public class TenantAppService : ITenantAppService
{
    private readonly ITenantDomainService _tenantDomainService;

    public TenantAppService(ITenantDomainService tenantDomainService)
    {
        _tenantDomainService = tenantDomainService;
    }

    public Task CreateOrUpdateAsync(CreateOrUpdateTenantInput input)
    {
        if (!input.Id.HasValue)
        {
            return _tenantDomainService.CreateAsync(input);
        }
        return _tenantDomainService.UpdateAsync(input);
    }
}