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

    public Task CreateOrUpdateCatalogAsync(CreateOrUpdateCatalogInput input)
    {
        if (!input.Id.HasValue)
        {
            return _menuDomainService.CreateCatalogAsync(input);
        }
        return _menuDomainService.UpdateCatalogAsync(input);
    }

    public Task CreateOrUpdateMenuAsync(CreateOrUpdateMenuInput input)
    {
        if (!input.Id.HasValue)
        {
            return _menuDomainService.CreateMenuAsync(input);
        }
        return _menuDomainService.UpdateMenuAsync(input);
    }
}