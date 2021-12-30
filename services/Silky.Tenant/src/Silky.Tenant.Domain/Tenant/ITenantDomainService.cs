using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Tenant.Application.Contracts.Tenant.Dtos;

namespace Silky.Tenant.Domain;

public interface ITenantDomainService
{
    IRepository<Tenant> TenantRepository { get; }
    Task CreateAsync(CreateTenantInput input);
    Task UpdateAsync(UpdateTenantInput input);
}