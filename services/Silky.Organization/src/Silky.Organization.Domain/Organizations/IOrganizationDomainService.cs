using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain.Organizations;

public interface IOrganizationDomainService : IScopedDependency
{
    IRepository<Organization> OrganizationRepository { get; }
    Task CreateAsync(CreateOrUpdateOrganizationInput input);
    Task UpdateAsync(CreateOrUpdateOrganizationInput input);
    Task DeleteAsync(long id);
    Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync();
}