using System;
using System.Threading.Tasks;
using Mapster;
using Silky.Core.Exceptions;
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

    public async Task<GetTenantOutput> GetAsync(Guid id)
    {
        var tenant = await _tenantDomainService.TenantRepository.FindOrDefaultAsync(id);
        if (tenant == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的租户信息");
        }

        return tenant.Adapt<GetTenantOutput>();
    }
}