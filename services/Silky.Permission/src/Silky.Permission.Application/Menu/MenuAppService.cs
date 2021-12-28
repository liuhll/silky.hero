using System.Threading.Tasks;
using Mapster;
using Silky.Core.Exceptions;
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

    public async Task<GetMenuOutput> GetAsync(long id)
    {
        var menu = await _menuDomainService.MenuRepository.FindOrDefaultAsync(id);
        if (menu == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的菜单信息");
        }

        return menu.Adapt<GetMenuOutput>();
    }
}