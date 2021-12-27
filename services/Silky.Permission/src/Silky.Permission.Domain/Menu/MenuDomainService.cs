using System.Threading.Tasks;
using Mapster;
using Silky.Core;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Permission.Application.Contracts.Menu.Dtos;

namespace Silky.Permission.Domain.Menu;

public class MenuDomainService : IMenuDomainService, IScopedDependency
{
    public MenuDomainService(IRepository<Menu> menuRepository)
    {
        MenuRepository = menuRepository;
    }

    public IRepository<Menu> MenuRepository { get; }

    public async Task CreateCatalogAsync(CreateOrUpdateCatalogInput input)
    {
        var exsitCatalog = await MenuRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (exsitCatalog != null)
        {
            throw new UserFriendlyException($"已经存在{input.Name}目录,目录名称不允许重复");
        }

        exsitCatalog = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
        if (exsitCatalog != null)
        {
            throw new UserFriendlyException($"已经存在{input.PermissionCode}目录,目录标识不允许重复");
        }

        var catalog = input.Adapt<Menu>();
        await MenuRepository.InsertAsync(catalog);
    }

    public async Task UpdateCatalogAsync(CreateOrUpdateCatalogInput input)
    {
        Check.NotNull(input.Id, nameof(input.Id));
        var catalog = await MenuRepository.FindOrDefaultAsync(input.Id);
        if (catalog == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}目录");
        }

        if (!input.Name.Equals(catalog.Name))
        {
            var exsitCatalog = await MenuRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (exsitCatalog != null)
            {
                throw new UserFriendlyException($"已经存在{input.Name}目录,目录名称不允许重复");
            }
        }

        if (!input.PermissionCode.Equals(catalog.PermissionCode))
        {
            var exsitCatalog = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
            if (exsitCatalog != null)
            {
                throw new UserFriendlyException($"已经存在{input.PermissionCode}目录,目录标识不允许重复");
            }
        }

        catalog = input.Adapt(catalog);
        await MenuRepository.UpdateAsync(catalog);
    }

    public async Task CreateMenuAsync(CreateOrUpdateMenuInput input)
    {
        var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (exsitMenu != null)
        {
            throw new UserFriendlyException($"已经存在{input.Name}菜单,菜单名称不允许重复");
        }

        exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
        if (exsitMenu != null)
        {
            throw new UserFriendlyException($"已经存在{input.PermissionCode}菜单,菜单标识不允许重复");
        }

        var menu = input.Adapt<Menu>();
        await MenuRepository.InsertAsync(menu);
    }

    public async Task UpdateMenuAsync(CreateOrUpdateMenuInput input)
    {
        Check.NotNull(input.Id, nameof(input.Id));
        var menu = await MenuRepository.FindOrDefaultAsync(input.Id);
        if (menu == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}菜单");
        }

        if (!input.Name.Equals(menu.Name))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.Name}菜单,菜单名称不允许重复");
            }
        }

        if (!input.PermissionCode.Equals(menu.PermissionCode))
        {
            var exsitMenu = await MenuRepository.FirstOrDefaultAsync(p => p.PermissionCode == input.PermissionCode);
            if (exsitMenu != null)
            {
                throw new UserFriendlyException($"已经存在{input.PermissionCode}菜单,菜单标识不允许重复");
            }
        }

        menu = input.Adapt(menu);
        await MenuRepository.UpdateAsync(menu);
    }
}