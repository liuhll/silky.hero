using System.Threading.Tasks;
using Mapster;
using Silky.Core;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Permission.Domain.Shared.Menu;

namespace Silky.Permission.Domain.Menu;

public class MenuDomainService : IMenuDomainService, IScopedDependency
{
    public MenuDomainService(IRepository<Menu> menuRepository)
    {
        MenuRepository = menuRepository;
    }

    public IRepository<Menu> MenuRepository { get; }

    public async Task CreateAsync(CreateOrUpdateMenuInput input)
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

    public async Task UpdateAsync(CreateOrUpdateMenuInput input)
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

    private async Task CheckButtonInput(CreateOrUpdateMenuInput input, Menu menu = null)
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

    private async Task CheckMenuInput(CreateOrUpdateMenuInput input, Menu menu = null)
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
                throw new UserFriendlyException($"已经存在{input.Name}菜单,名称不允许重复");
            }
        }

        if (menu == null || !input.PermissionCode.Equals(menu?.PermissionCode))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.PermissionCode}目录,权限标识不允许重复");
            }
        }
    }

    private async Task CheckCatalogInput(CreateOrUpdateMenuInput input, Menu menu = null)
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

        if (menu == null || !input.PermissionCode.Equals(menu?.PermissionCode))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.PermissionCode}目录,权限标识不允许重复");
            }
        }
    }
}