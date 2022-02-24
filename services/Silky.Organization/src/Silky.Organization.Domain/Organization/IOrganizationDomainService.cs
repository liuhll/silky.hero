using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain;

public interface IOrganizationDomainService : IScopedDependency
{
    IRepository<Organization> OrganizationRepository { get; }
    Task CreateAsync(CreateOrganizationInput input);
    Task UpdateAsync(UpdateOrganizationInput input);
    Task DeleteTryAsync(long id);
    Task DeleteConfirmAsync(long id);
    Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync();
    Task<IEnumerable<Organization>> GetChildrenOrganizationsAsync(long organizationId, bool includeSelf = true);

    Task SetAllocationRoleListAsync(long id, long[] roleIds);
}