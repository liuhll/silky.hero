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
    Task DeleteAsync(long id);
    Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync();
}