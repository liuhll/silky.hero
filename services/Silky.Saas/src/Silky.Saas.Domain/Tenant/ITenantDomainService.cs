using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Saas.Application.Contracts.Tenant.Dtos;

namespace Silky.Saas.Domain;

public interface ITenantDomainService
{
    IRepository<Tenant> TenantRepository { get; }
    
    Task CreateTryAsync(CreateTenantInput input);
   
    Task CreateConfirmAsync(CreateTenantInput input);
    
    Task CreateCancelAsync(CreateTenantInput input);
    
    Task UpdateAsync(UpdateTenantInput input);
    
}