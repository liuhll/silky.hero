using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain.Organizations;

public interface IOrganizationDomainService : IScopedDependency
{
    Task CreateAsync(CreateOrUpdateOrganizationInput input);
    Task UpdateAsync(CreateOrUpdateOrganizationInput input);
    Task DeleteAsync(long id);
    Task<ICollection<Organization>> GetTreeAsync(long id);
}