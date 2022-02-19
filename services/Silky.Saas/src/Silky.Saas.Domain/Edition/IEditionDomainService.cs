using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Saas.Application.Contracts.Edition.Dtos;

namespace Silky.Saas.Domain;

public interface IEditionDomainService
{
    IRepository<Edition> EditionRepository { get; }
    
    Task CreateAsync(CreateEditionInput input);
    
    Task UpdateAsync(UpdateEditionInput input);
    
    Task DeleteAsync(long id);
    
    Task<GetEditionEditOutput> GetAsync(long id);
}