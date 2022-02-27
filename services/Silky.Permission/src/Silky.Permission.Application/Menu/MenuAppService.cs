using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Exceptions;
using Silky.Hero.Common.Enums;
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

    public Task CreateAsync(CreateMenuInput input)
    {
        return _menuDomainService.CreateAsync(input);
    }

    public Task<bool> CheckAsync(CheckMenuInput input)
    {
        return _menuDomainService.MenuRepository.AnyAsync(p => p.Id != input.Id && p.ParentId == input.ParentId && p.Name == input.Name);
    }

    public Task UpdateAsync(UpdateMenuInput input)
    {
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
        await _menuDomainService.DeleteAsync(id);
    }

    public async Task<ICollection<GetMenuTreeOutput>> GetTreeAsync(string name)
    {
        var menuTree = await _menuDomainService.GetTreeAsync(name);
        var menuList = menuTree.Adapt<ICollection<GetMenuTreeOutput>>();
        return menuList;
    }

    public async Task<bool> HasMenuAsync(long menuId)
    {
        var menu = await _menuDomainService.MenuRepository.FindOrDefaultAsync(menuId);
        return menu != null;
    }

    public async Task<ICollection<string>> GetPermissions(List<long> menuIds)
    {
        var permissionCodes = await (
                await _menuDomainService
                    .MenuRepository
                    .GetCurrentTenantMenus())
            .Where(p => menuIds.Contains(p.Id))
            .Where(p => p.PermissionCode != null)
            .Select(p => p.PermissionCode)
            .ProjectToType<string>()
            .ToListAsync();
        return permissionCodes;
    }

    public Task<ICollection<GetMenuOutput>> GetMenusAsync(long[] menuIds, bool includeParents = true)
    {
        return _menuDomainService.GetMenusAsync(menuIds, includeParents);
    }

    public async Task<long[]> GetAllMenuIdsAsync(bool onlyValid = true)
    {
        var menuIds = await _menuDomainService.MenuRepository
            .AsQueryable(false)
            .Where(onlyValid, m => m.Status == Status.Valid)
            .Select(p => p.Id)
            .ToArrayAsync();
        return menuIds;
    }
}