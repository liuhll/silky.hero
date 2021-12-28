using System.Threading.Tasks;
using Silky.Permission.Application.Contracts.Menu.Dtos;

namespace Silky.Permission.Domain.Menu;

public interface IMenuDomainService
{

    Task CreateAsync(CreateOrUpdateMenuInput input);
    Task UpdateAsync(CreateOrUpdateMenuInput input);

  
}