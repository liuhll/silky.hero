using System.Threading.Tasks;
using Silky.Permission.Application.Contracts.Menu;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Permission.Domain.Menu;

namespace Silky.Permission.Application.Menu;

public class MenuAppService : IMenuAppService
{
    private readonly IMenuDomainService _menuDomainService;

    public MenuAppService(IMenuDomainService menuDomainService)
    {
        _menuDomainService = menuDomainService;
    }

    public Task CreateOrUpdateAsync(CreateOrUpdateMenuInput input)
    {
        if (!input.Id.HasValue)
        {
            return _menuDomainService.CreateAsync(input);
        }

        return _menuDomainService.UpdateAsync(input);
    }
}