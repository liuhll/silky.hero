using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Permission.Application.Contracts.Menu.Dtos;

namespace Silky.Permission.Domain.Menu;

public interface IMenuDomainService
{
    IRepository<Menu> MenuRepository { get; }
    Task CreateAsync(CreateMenuInput input);
    Task UpdateAsync(UpdateMenuInput input);

    Task<ICollection<Menu>> GetTreeAsync(string name);
    
    Task DeleteAsync(long id);

    Task<ICollection<GetMenuOutput>> GetMenusAsync(long[] menuIds, bool includeParents);
}