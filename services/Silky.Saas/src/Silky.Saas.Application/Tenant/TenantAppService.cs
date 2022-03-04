using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Caching;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.Hero.Common.Enums;
using Silky.Saas.Application.Contracts.Tenant;
using Silky.Saas.Application.Contracts.Tenant.Dtos;
using Silky.Saas.Domain;
using Silky.Saas.Domain.Shared.Tenant;
using Silky.Transaction.Tcc;

namespace Silky.Saas.Application.Tenant;

public class TenantAppService : ITenantAppService
{
    private readonly ITenantDomainService _tenantDomainService;

    public TenantAppService(ITenantDomainService tenantDomainService)
    {
        _tenantDomainService = tenantDomainService;
    }

    [TccTransaction(ConfirmMethod = "CreateConfirmAsync", CancelMethod = "CreateCancelAsync")]
    public Task CreateAsync(CreateTenantInput input)
    {
        return _tenantDomainService.CreateTryAsync(input);
    }

    public Task CreateConfirmAsync(CreateTenantInput input)
    {
        return _tenantDomainService.CreateConfirmAsync(input);
    }

    public Task CreateCancelAsync(CreateTenantInput input)
    {
        return _tenantDomainService.CreateCancelAsync(input);
    }

    public async Task UpdateAsync(UpdateTenantInput input)
    {
        await _tenantDomainService.UpdateAsync(input);
    }

    public async Task<bool> CheckAsync(CheckTenantInput input)
    {
        var exsit = false;
        switch (input.NameType)
        {
            case TenantNameType.Name:
                exsit = await _tenantDomainService.TenantRepository.AnyAsync(p => p.Name == input.Name && p.Id != input.Id, false);
                break;
            case TenantNameType.RealName:
                exsit = await _tenantDomainService.TenantRepository.AnyAsync(p => p.RealName == input.Name && p.Id != input.Id, false);
                break;
            default:
                throw new UserFriendlyException("请指定正确的类型参数");
        }

        return exsit;
    }

    public async Task<GetTenantOutput> GetAsync(long id)
    {
        var tenant = await _tenantDomainService.TenantRepository.FindOrDefaultAsync(id);
        if (tenant == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的租户信息");
        }

        var tenantOutput = tenant.Adapt<GetTenantOutput>();
        await tenantOutput.SetEditionName();
        return tenantOutput;
    }

    public Task DeleteAsync(long id)
    {
        return _tenantDomainService.DeleteAsync(id);
    }

    public async Task<PagedList<GetTenantPageOutput>> GetPageAsync(GetTenantPageInput input)
    {
        var page = await _tenantDomainService.TenantRepository
            .AsQueryable(false)
            .Where(!input.Name.IsNullOrEmpty(), p => p.Name == input.Name)
            .Where(input.Status.HasValue, p => p.Status == input.Status)
            .Where(input.EditionId.HasValue, p => p.EditionId == input.EditionId)
            .OrderByDescending(p => p.Sort)
            .ProjectToType<GetTenantPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        foreach (var item in page.Items)
        {
            await item.SetEditionName();
        }

        return page;
    }

    public async Task<ICollection<GetTenantOutput>> GetAllAsync()
    {
        return await _tenantDomainService.TenantRepository
            .AsQueryable(false)
            .Where(p => p.Status == Status.Valid)
            .OrderByDescending(p => p.Sort)
            .ProjectToType<GetTenantOutput>()
            .ToListAsync();
    }

    public Task<GetTenantOutput> GetByNameAsync(string name)
    {
        return _tenantDomainService
            .TenantRepository
            .AsQueryable(false)
            .ProjectToType<GetTenantOutput>()
            .FirstOrDefaultAsync(p => p.Name == name);
    }
}