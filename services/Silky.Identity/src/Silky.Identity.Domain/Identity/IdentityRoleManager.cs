using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.EntityFrameworkCore;
using Silky.Permission.Application.Contracts.Menu;

namespace Silky.Identity.Domain;

public class IdentityRoleManager : RoleManager<IdentityRole>
{
    public IIdentityRoleRepository RoleRepository { get; }
    public IRepository<IdentityRoleMenu> RoleMenuRepository { get; }
    private readonly IMenuAppService _menuAppService;

    public IdentityRoleManager(IdentityRoleStore store,
        IEnumerable<IRoleValidator<IdentityRole>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<IdentityRoleManager> logger,
        IIdentityRoleRepository roleRepository,
        IRepository<IdentityRoleMenu> roleMenuRepository,
        IMenuAppService menuAppService)
        : base(store,
            roleValidators,
            keyNormalizer,
            errors,
            logger)
    {
        RoleRepository = roleRepository;
        RoleMenuRepository = roleMenuRepository;
        _menuAppService = menuAppService;
    }

    public virtual async Task<IdentityRole> GetByIdAsync(long id)
    {
        var role = await Store.FindByIdAsync(id.ToString(), CancellationToken);
        if (role == null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), id);
        }

        return role;
    }

    public override async Task<IdentityResult> SetRoleNameAsync(IdentityRole role, string name)
    {
        if (role.IsStatic && role.Name != name)
        {
            throw new BusinessException("静态角色标识不允许重命名");
        }

        return await base.SetRoleNameAsync(role, name);
    }

    public async Task<IdentityResult> SetRoleRealNameAsync(IdentityRole role, string realName)
    {
        if (role.IsStatic && role.RealName != realName)
        {
            throw new BusinessException("静态角色名称不允许重命名");
        }

        await ((IdentityRoleStore)Store).SetRoleRealNameAsync(role, realName, CancellationToken);
        return IdentityResult.Success;
    }

    public override async Task<IdentityResult> DeleteAsync(IdentityRole role)
    {
        if (role.IsStatic)
        {
            throw new BusinessException("静态角色名称不允许删除");
        }

        return await base.DeleteAsync(role);
    }

    public async Task<IdentityResult> SetRoleMenusAsync(IdentityRole role, ICollection<IdentityRoleMenu> roleMenus)
    {
        Check.NotNull(role, nameof(role));
        Check.NotNull(roleMenus, nameof(roleMenus));
        foreach (var roleMenu in roleMenus)
        {
            if (!await _menuAppService.HasMenuAsync(roleMenu.MenuId))
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NoMenu",
                    Description = $"不存在Id为{roleMenu.MenuId}的菜单"
                });
            }
        }

        var currentRoleMenus = await GetCurrentRoleMenusAsync(role);
        var result = await RemoveFromRoleMenusAsync(role, currentRoleMenus.Except(roleMenus).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }

        result = await AddToRoleMenusAsync(role, roleMenus.Except(currentRoleMenus).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }
        return IdentityResult.Success;
    }

    private Task<IdentityResult> AddToRoleMenusAsync(IdentityRole role, IEnumerable<IdentityRoleMenu> roleMenus)
    {
      
        role.AddMenus(roleMenus);
        return Task.FromResult(IdentityResult.Success);
    }

    private async Task<IdentityResult> RemoveFromRoleMenusAsync(IdentityRole role,
        IEnumerable<IdentityRoleMenu> roleMenus)
    {
        foreach (var roleMenu in roleMenus)
        {
            role.RemoveMenu(roleMenu.MenuId);
        }

        return IdentityResult.Success;
    }

    private async Task<ICollection<IdentityRoleMenu>> GetCurrentRoleMenusAsync(IdentityRole role)
    {
        return await RoleMenuRepository.Where(p => p.RoleId == role.Id).ToListAsync();
    }
}