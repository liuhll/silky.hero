using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Permission.Application.Contracts.Menu.Dtos;

namespace Silky.Permission.Domain.Menu;

public interface IMenuDomainService
{
    IRepository<Menu> MenuRepository { get; }
    Task CreateAsync(CreateOrUpdateMenuInput input);
    Task UpdateAsync(CreateOrUpdateMenuInput input);

  
}