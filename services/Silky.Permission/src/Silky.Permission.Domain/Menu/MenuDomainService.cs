using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Identity.Application.Contracts.Role;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Permission.Domain.Shared.Menu;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Permission.Domain.Menu;

public class MenuDomainService : IMenuDomainService, IScopedDependency
{
    private readonly IRoleAppService _roleAppService;
    private readonly IEditionAppService _editionAppService;

    public MenuDomainService(IRepository<Menu> menuRepository,
        IRoleAppService roleAppService,
        IEditionAppService editionAppService)
    {
        MenuRepository = menuRepository;
        _roleAppService = roleAppService;
        _editionAppService = editionAppService;
    }

    public IRepository<Menu> MenuRepository { get; }

    public async Task CreateAsync(CreateMenuInput input)
    {
        switch (input.Type)
        {
            case MenuType.Catalog:
                await CheckCatalogInput(input);
                break;
            case MenuType.Menu:
                await CheckMenuInput(input);
                break;
            case MenuType.Button:
                await CheckButtonInput(input);
                break;
        }

        var menu = input.Adapt<Menu>();
        await MenuRepository.InsertAsync(menu);
    }

    public async Task UpdateAsync(UpdateMenuInput input)
    {
        Check.NotNull(input.Id, nameof(input.Id));
        var menu = await MenuRepository.FindOrDefaultAsync(input.Id);
        if (menu == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}菜单");
        }

        switch (input.Type)
        {
            case MenuType.Catalog:
                await CheckCatalogInput(input, menu);
                break;
            case MenuType.Menu:
                await CheckMenuInput(input, menu);
                break;
            case MenuType.Button:
                await CheckButtonInput(input, menu);
                break;
        }

        menu = input.Adapt(menu);
        await MenuRepository.UpdateAsync(menu);
    }

    public async Task<ICollection<Menu>> GetTreeAsync(string name)
    {
        var menus = await MenuRepository.AsQueryable(false)
            .Where(!name.IsNullOrEmpty(), p => p.Name.Contains(name))
            .GetCurrentTenantMenus()
            .OrderByDescending(p => p.Sort)
            .ToListAsync();

        return menus.BuildTree();
    }

    public async Task DeleteAsync(long id)
    {
        var menu = await MenuRepository.FindOrDefaultAsync(id);
        if (menu == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的菜单信息");
        }

        var childrenMenus = await GetChildrenMenusAsync(id);
        if (await _roleAppService.CheckHasMenusAsync(childrenMenus.Select(p => p.Id).ToArray()))
        {
            throw new UserFriendlyException($"该菜单或存在子菜单被授权,无法删除");
        }

        await MenuRepository.DeleteAsync(childrenMenus);
    }

    public async Task<IEnumerable<Menu>> GetChildrenMenusAsync(long menuId,
        bool includeSelf = true)
    {
        var menus = await MenuRepository
            .AsQueryable()
            .OrderByDescending(p => p.Sort).ToListAsync();
        return menus.GetChildrenMenus(menuId, includeSelf);
    }

    private async Task CheckButtonInput(MenuDtoBase input, Menu menu = null)
    {
        if (!input.ParentId.HasValue)
        {
            throw new ValidationException("父级菜单不允许为空");
        }

        if (menu == null || !input.PermissionCode.Equals(menu?.PermissionCode))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.PermissionCode}按钮,权限标识不允许重复");
            }
        }
    }

    private async Task CheckMenuInput(MenuDtoBase input, Menu menu = null)
    {
        if (input.RoutePath.IsNullOrEmpty())
        {
            throw new ValidationException("路由地址不允许为空");
        }

        // if (input.Icon.IsNullOrEmpty())
        // {
        //     throw new ValidationException("图标不允许为空");
        // }

        if (menu == null || !input.Name.Equals(menu?.Name))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.Name}菜单,名称不允许重复");
            }
        }

        if (!input.PermissionCode.IsNullOrEmpty() &&
            (menu == null || !input.PermissionCode.Equals(menu?.PermissionCode)))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.PermissionCode}菜单,权限标识不允许重复");
            }
        }
    }

    private async Task CheckCatalogInput(MenuDtoBase input, Menu menu = null)
    {
        if (input.RoutePath.IsNullOrEmpty())
        {
            throw new ValidationException("路由地址不允许为空");
        }

        if (input.Icon.IsNullOrEmpty())
        {
            throw new ValidationException("图标不允许为空");
        }

        if (menu == null || !input.Name.Equals(menu?.Name))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.Name}目录,名称不允许重复");
            }
        }

        if (!input.PermissionCode.IsNullOrEmpty() &&
            (menu == null || !input.PermissionCode.Equals(menu?.PermissionCode)))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.PermissionCode}目录,权限标识不允许重复");
            }
        }
    }

    public async Task<ICollection<GetMenuOutput>> GetMenusAsync(long[] menuIds, bool includeParents)
    {
        if (!includeParents)
        {
            return await MenuRepository
                .AsQueryable(false)
                .GetCurrentTenantMenus()
                .Where(p => menuIds.Contains(p.Id))
                .ProjectToType<GetMenuOutput>().ToListAsync();
        }
        else
        {
            var allMenus = await MenuRepository
                .AsQueryable(false)
                .GetCurrentTenantMenus()
                .ProjectToType<GetMenuOutput>()
                .ToArrayAsync();
            var menus = allMenus.Where(p => menuIds.Contains(p.Id));
            var parentIds = menus.Where(p => p.ParentId.HasValue).Select(p => p.ParentId.Value).ToArray();
            return GetMenusIncludeParents(allMenus, menus, parentIds)
                .Distinct()
                .OrderByDescending(p => p.Sort)
                .ToList();
        }
    }

    private ICollection<GetMenuOutput> GetMenusIncludeParents(GetMenuOutput[] allMenus,
        IEnumerable<GetMenuOutput> menus, long[] parnetIds)
    {
        var includeParentMenus = new List<GetMenuOutput>();
        includeParentMenus.AddRange(menus);
        if (parnetIds != null && parnetIds.Any())
        {
            var parnetMenus = allMenus.Where(p => parnetIds.Contains(p.Id));
            includeParentMenus.AddRange(parnetMenus);
            var parnetMenuParentIds =
                parnetMenus.Where(p => p.ParentId.HasValue).Select(p => p.ParentId.Value).ToArray();
            return GetMenusIncludeParents(allMenus, includeParentMenus, parnetMenuParentIds);
        }

        return includeParentMenus;
    }
}