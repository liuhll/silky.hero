using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Silky.Core.Exceptions;
using Silky.Core.Extensions.Collections.Generic;
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

    public async Task DeleteAsync(long id)
    {
        var menu = await _menuDomainService.MenuRepository.FindOrDefaultAsync(id);
        if (menu == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的菜单信息");
        }

        var children = _menuDomainService.MenuRepository.Where(p => p.ParentId == id);
        if (children.Any())
        {
            throw new UserFriendlyException($"请先删除子菜单");
        }

        // todo 确认菜单是否被分配

        await _menuDomainService.MenuRepository.DeleteAsync(menu);
    }

    public async Task<PagedList<GetMenuPageOutput>> GetPageAsync(GetMenuPageInput input)
    {
        var menuTree = await _menuDomainService.GetTreeAsync();
        var menuList = menuTree.Adapt<ICollection<GetMenuPageOutput>>();
        var menuPageList = menuList.ToPagedList(input.PageIndex, input.PageSize);
        return menuPageList;
    }
}