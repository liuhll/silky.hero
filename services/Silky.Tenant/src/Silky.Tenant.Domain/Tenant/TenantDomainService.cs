using System.Threading.Tasks;
using Mapster;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Tenant.Application.Contracts.Tenant.Dtos;

namespace Silky.Tenant.Domain;

public class TenantDomainService : ITenantDomainService, IScopedDependency
{
    public TenantDomainService(IRepository<Tenant> tenantRepository)
    {
        TenantRepository = tenantRepository;
    }

    public IRepository<Tenant> TenantRepository { get; }

    public async Task CreateAsync(CreateTenantInput input)
    {
        var existTenant = await TenantRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (existTenant != null)
        {
            throw new UserFriendlyException($"已经存在{input.Name}的租户信息");
        }

        var tenant = input.Adapt<Tenant>();
        await TenantRepository.InsertAsync(tenant);
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
    }
}