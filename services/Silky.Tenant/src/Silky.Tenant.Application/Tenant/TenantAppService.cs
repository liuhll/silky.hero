using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.Hero.Common.Enums;
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

    public async Task<GetTenantOutput> GetAsync(long id)
    {
        var tenant = await _tenantDomainService.TenantRepository.FindOrDefaultAsync(id);
        if (tenant == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的租户信息");
        }

        return tenant.Adapt<GetTenantOutput>();
    }

    public async Task DeleteAsync(long id)
    {
        var tenant = await _tenantDomainService.TenantRepository.FindOrDefaultAsync(id);
        if (tenant == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的租户信息");
        }

        await _tenantDomainService.TenantRepository.DeleteAsync(tenant);
    }

    public async Task<PagedList<GetTenantPageOutput>> GetPageAsync(GetTenantPageInput input)
    {
        return await _tenantDomainService.TenantRepository
            .Where(!input.Name.IsNullOrEmpty(), p => p.Name == input.Name)
            .Where(input.Status.HasValue, p => p.Status == input.Status)
            .AsNoTracking()
            .ProjectToType<GetTenantPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
    }

    public async Task<ICollection<GetTenantOutput>> GetAllAsync()
    {
        return await _tenantDomainService.TenantRepository
            .Where(p => p.Status == Status.Valid)
            .AsNoTracking()
            .ProjectToType<GetTenantOutput>()
            .ToListAsync();
    }
}