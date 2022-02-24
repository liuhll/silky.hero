using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.EntityFrameworkCore;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Domain.Extensions;
using Silky.Identity.Domain.Shared;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Permission.Application.Contracts.Menu;

namespace Silky.Identity.Domain;

public class IdentityRoleManager : RoleManager<IdentityRole>
{
    public IIdentityRoleRepository RoleRepository { get; }
    public IRepository<IdentityRoleMenu> RoleMenuRepository { get; }
    public IRepository<IdentityRoleOrganization> RoleOrganizationRepository { get; }
    
    public IMenuAppService MenuAppService { get; }
    
    private readonly IOrganizationAppService _organizationAppService;

    public IdentityRoleManager(IdentityRoleStore store,
        IEnumerable<IRoleValidator<IdentityRole>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<IdentityRoleManager> logger,
        IIdentityRoleRepository roleRepository,
        IRepository<IdentityRoleMenu> roleMenuRepository,
        IRepository<IdentityRoleOrganization> roleOrganizationRepository,
        IMenuAppService menuAppService,
        IOrganizationAppService organizationAppService)
        : base(store,
            roleValidators,
            keyNormalizer,
            errors,
            logger)
    {
        RoleRepository = roleRepository;
        RoleMenuRepository = roleMenuRepository;
        RoleOrganizationRepository = roleOrganizationRepository;
        MenuAppService = menuAppService;
        _organizationAppService = organizationAppService;
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
            throw new BusinessException("静态角色不允许删除");
        }

        return await base.DeleteAsync(role);
    }

    public async Task<IdentityResult> SetRoleMenusAsync(IdentityRole role, ICollection<IdentityRoleMenu> roleMenus)
    {
        Check.NotNull(role, nameof(role));
        Check.NotNull(roleMenus, nameof(roleMenus));
        foreach (var roleMenu in roleMenus)
        {
            if (!await MenuAppService.HasMenuAsync(roleMenu.MenuId))
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

    public async Task<IdentityResult> SetRoleDataRangeAsync(IdentityRole role, DataRange dataRange,
        List<IdentityRoleOrganization> roleOrganizations)
    {
        if (dataRange == DataRange.CustomOrganization && roleOrganizations?.Any() == false)
        {
            throw new BusinessException("请指定自定义的数据权限范围");
        }

        role.SetDataRange(dataRange);
        if (dataRange == DataRange.CustomOrganization)
        {
            foreach (var roleOrganization in roleOrganizations)
            {
                if (!await _organizationAppService.HasOrganizationAsync(roleOrganization.OrganizationId))
                {
                    return IdentityResult.Failed(new IdentityError()
                    {
                        Code = "NoOrganization",
                        Description = $"不存在Id为{roleOrganization.OrganizationId}的组织机构"
                    });
                }
            }

            var currentRoleOrganizations = await GetCurrentRoleOrganizationsAsync(role);
            var result =
                await RemoveFromRoleOrganizationsAsync(role,
                    currentRoleOrganizations.Except(roleOrganizations).Distinct());
            if (!result.Succeeded)
            {
                return result;
            }

            result = await AddToRoleOrganizationsAsync(role,
                roleOrganizations.Except(currentRoleOrganizations).Distinct());
            if (!result.Succeeded)
            {
                return result;
            }
        }

        return IdentityResult.Success;
    }

    private Task<IdentityResult> AddToRoleOrganizationsAsync(IdentityRole role,
        IEnumerable<IdentityRoleOrganization> roleOrganizations)
    {
        role.AddCustomOrganizationDataRanges(roleOrganizations);
        return Task.FromResult(IdentityResult.Success);
    }

    private async Task<IdentityResult> RemoveFromRoleOrganizationsAsync(IdentityRole role,
        IEnumerable<IdentityRoleOrganization> roleOrganizations)
    {
        foreach (var roleOrganization in roleOrganizations)
        {
            role.RemoveCustomOrganizationDataRange(roleOrganization.OrganizationId);
        }

        return IdentityResult.Success;
    }

    private async Task<ICollection<IdentityRoleOrganization>> GetCurrentRoleOrganizationsAsync(IdentityRole role)
    {
        return await RoleOrganizationRepository.Where(p => p.RoleId == role.Id).ToListAsync();
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

    public async Task<ICollection<string>> GetPermissionsAsync(long roleId)
    {
        var roleMenuIds = await RoleMenuRepository.AsQueryable(false)
            .Where(p => p.RoleId == roleId)
            .Select(p => p.MenuId)
            .ToListAsync();
        var permissions = await MenuAppService.GetPermissions(roleMenuIds);
        return permissions;
    }

    public async Task<bool> CheckHasMenusAsync(long[] menuIds)
    {
        Check.NotNull(menuIds, nameof(menuIds));
        return (await RoleMenuRepository.AsQueryable(false).CountAsync(p => menuIds.Contains(p.MenuId))) > 0;
    }

    public async Task<GetRoleDetailOutput> GetRoleOutputByIdAsync(long id)
    {
        var role = await RoleRepository
            .AsQueryable(false)
            .Include(p => p.Menus)
            .Include(p => p.CustomOrganizationDataRanges)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (role == null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), id);
        }
        var roleOutput = role.Adapt<GetRoleDetailOutput>();
        var roleMenuIds = role.Menus.Select(p => p.MenuId).ToArray();
        var menus = await MenuAppService.GetMenusAsync(roleMenuIds);
        var frontendMenus = menus.MapFrontendMenus(true);
        roleOutput.Menus = frontendMenus.BuildTree().Adapt<ICollection<GetRoleMenuTreeOutput>>();
        foreach (var customOrganizationOutput in roleOutput.CustomOrganizationDataRanges)
        {
           await customOrganizationOutput.SetOrganizationName();
        }
        return roleOutput;
    }
}