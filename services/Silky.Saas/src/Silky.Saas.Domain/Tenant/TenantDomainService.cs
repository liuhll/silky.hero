using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Caching;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.Extensions;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.User;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Saas.Application.Contracts.Edition.Dtos;
using Silky.Saas.Application.Contracts.Tenant.Dtos;

namespace Silky.Saas.Domain;

public class TenantDomainService : ITenantDomainService, IScopedDependency
{
    private readonly IUserAppService _userAppService;
    private readonly IRoleAppService _roleAppService;
    private readonly IDistributedCache _distributedCache;
    private readonly IDistributedCacheKeyNormalizer _distributedCacheKeyNormalizer;
    public TenantDomainService(IRepository<Tenant> tenantRepository, 
        IUserAppService userAppService, 
        IRoleAppService roleAppService, 
        IDistributedCache distributedCache, 
        IDistributedCacheKeyNormalizer distributedCacheKeyNormalizer)
    {
        TenantRepository = tenantRepository;
        _userAppService = userAppService;
        _roleAppService = roleAppService;
        _distributedCache = distributedCache;
        _distributedCacheKeyNormalizer = distributedCacheKeyNormalizer;
    }

    public IRepository<Tenant> TenantRepository { get; }

    public async Task CreateTryAsync(CreateTenantInput input)
    {
        var existTenant = await TenantRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (existTenant != null)
        {
            throw new UserFriendlyException($"已经存在{input.Name}的租户信息");
        }
        var tenant = input.Adapt<Tenant>();
        var tenantEntry = await TenantRepository.InsertNowAsync(tenant);
        var roleName = await _roleAppService.CreateSuperRoleAsync(tenantEntry.Entity.Id, input.SuperRoleName, input.SuperRoleRealName);
        var superUserInput = new CreateSuperUserInput()
        {
            TenantId = tenantEntry.Entity.Id,
            RoleName = roleName,
            UserName = input.SuperUserName,
            RealName = input.SuperRealName,
            Email = input.SuperUserEmail,
            MobilePhone = input.SuperUserMobilePhone,
            JobNumber = input.SuperUserJobNumber,
            Password = input.SuperPassword,

        };
        await _userAppService.CreateSuperUserAsync(superUserInput);

    }

    public async Task CreateConfirmAsync(CreateTenantInput input)
    {
        var tenant = await TenantRepository.FirstAsync(p=> p.Name == input.Name);
        tenant.Status = input.Status;
        await TenantRepository.UpdateNowAsync(tenant);
    }

    public async Task CreateCancelAsync(CreateTenantInput input)
    {
        var tenant = await TenantRepository.FirstOrDefaultAsync(p=> p.Name == input.Name);
        if (tenant != null)
        {
            await TenantRepository.DeleteNowAsync(tenant);
        }
    }

    public async Task UpdateAsync(UpdateTenantInput input)
    {
        var tenant = await TenantRepository.FindOrDefaultAsync(input.Id);
        if (tenant == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}的租户信息");
        }

        if (!tenant.Name.Equals(input.Name))
        {
            var existTenant = await TenantRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (existTenant != null)
            {
                throw new UserFriendlyException($"已经存在{input.Name}的租户信息");
            }
        }

        tenant = input.Adapt(tenant);
        await TenantRepository.UpdateAsync(tenant);
        await RemoveCacheAsync(tenant.Id);
    }

    public async Task DeleteAsync(long id)
    {
        var tenant = await TenantRepository.FindOrDefaultAsync(id);
        if (tenant == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的租户信息");
        }

        await TenantRepository.DeleteAsync(tenant);
        await RemoveCacheAsync(tenant.Id);
    }

    private async Task RemoveCacheAsync(long tenantId)
    {
        await _distributedCache.RemoveMatchKeyAsync(typeof(ICollection<GetCurrentUserMenuOutput>),"CurrentUserMenus:*");
        await _distributedCache.RemoveMatchKeyAsync(typeof(string[]),"CurrentUserPermissionCodes:*");
        await _distributedCache.RemoveMatchKeyAsync(typeof(GetEditionFeatureOutput),
            _distributedCacheKeyNormalizer.NormalizeTenantKey("featureCode:*",
                typeof(GetEditionFeatureOutput).FullName,
                tenantId));
    }
}