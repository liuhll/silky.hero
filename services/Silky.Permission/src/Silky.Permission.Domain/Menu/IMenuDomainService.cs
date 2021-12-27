using System.Threading.Tasks;
using Silky.Permission.Application.Contracts.Menu.Dtos;

namespace Silky.Permission.Domain.Menu;

public interface IMenuDomainService
{
    Task CreateCatalogAsync(CreateOrUpdateCatalogInput input);
    Task UpdateCatalogAsync(CreateOrUpdateCatalogInput input);
    Task CreateMenuAsync(CreateOrUpdateMenuInput input);
    Task UpdateMenuAsync(CreateOrUpdateMenuInput input);
    
}