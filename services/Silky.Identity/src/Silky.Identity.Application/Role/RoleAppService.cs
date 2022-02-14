using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Account.Application.Contracts.Account.Dtos;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.EntityFrameworkCore;
using Silky.Hero.Common.Extensions;
using Silky.Identity.Application.Contracts.User.Dtos;
using IdentityRole = Silky.Identity.Domain.IdentityRole;

namespace Silky.Identity.Application.Role;

public class RoleAppService : IRoleAppService
{
    private readonly IdentityRoleManager _roleManager;
    private readonly IRepository<IdentityUserRole> _userRoleRepository;
    private readonly ISession _session;
    private readonly IDistributedCache _distributedCache;
    
    public RoleAppService(IdentityRoleManager roleManager,
        IDistributedCache distributedCache, 
        IRepository<IdentityUserRole> userRoleRepository)
    {
        _roleManager = roleManager;
        _distributedCache = distributedCache;
        _userRoleRepository = userRoleRepository;
        _session = NullSession.Instance;
    }

    [UnitOfWork]
    public async Task CreateAsync(CreateRoleInput input)
    {
        var role = new IdentityRole(input.Name, input.RealName, _session.TenantId);
        await UpdateRoleByInput(role, input);
        (await _roleManager.CreateAsync(role)).CheckErrors();
    }

    public async Task UpdateAsync(UpdateRoleInput input)
    {
        var role = await _roleManager.GetByIdAsync(input.Id);
        await UpdateRoleByInput(role, input);
        (await _roleManager.UpdateAsync(role)).CheckErrors();
        await RemoveUserRoleCacheAsync(role.Id);
    }

    public Task<GetRoleOutput> GetAsync(long id)
    {
        return _roleManager.GetRoleOutputByIdAsync(id);
    }

    public async Task DeleteAsync(long id)
    {
        var role = await _roleManager.GetByIdAsync(id);
        (await _roleManager.DeleteAsync(role)).CheckErrors();
        await RemoveUserRoleCacheAsync(role.Id);
    }

    public async Task SetMenusAsync(UpdateRoleMenuInput input)
    {
        var role = await _roleManager.RoleRepository
            .Include(p => p.Menus)
            .FirstOrDefaultAsync(p => p.Id == input.Id);
        if (role == null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), input.Id);
        }

        (await _roleManager.SetRoleMenusAsync(role,
            input.MenuIds?.Select(mId => new IdentityRoleMenu(role.Id, mId, role.TenantId)).ToList())).CheckErrors();
        (await _roleManager.UpdateAsync(role)).CheckErrors();
        await RemoveUserRoleCacheAsync(role.Id);
    }

    public async Task<GetRoleMenuOutput> GetMenusAsync(long id)
    {
        var role = await _roleManager.RoleRepository
            .AsQueryable(false)
            .Include(p => p.Menus)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (role == null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), id);
        }

        return role.Adapt<GetRoleMenuOutput>();
    }

    public async Task SetDataRangeAsync(UpdateRoleDataRangeInput input)
    {
        var role = await _roleManager.RoleRepository
            .Include(p => p.CustomOrganizationDataRanges)
            .FirstOrDefaultAsync(p => p.Id == input.Id);
        if (role == null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), input.Id);
        }

        (await _roleManager.SetRoleDataRangeAsync(role, input.DataRange,
            input.CustomOrganizationIds?.Select(oId => new IdentityRoleOrganization(role.Id, oId, role.TenantId))
                .ToList())).CheckErrors();
        (await _roleManager.UpdateAsync(role)).CheckErrors();
        await RemoveUserRoleCacheAsync(role.Id);
    }

    public async Task<GetRoleDataRangeOutput> GetDataRangeAsync(long id)
    {
        var role = await _roleManager.RoleRepository
            .AsQueryable(false)
            .Include(p => p.CustomOrganizationDataRanges)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (role == null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), id);
        }

        return role.Adapt<GetRoleDataRangeOutput>();
    }

    public async Task<PagedList<GetRolePageOutput>> GetPageAsync(GetRolePageInput input)
    {
        var pageRoles = await _roleManager.RoleRepository
            .AsQueryable(false)
            .Where(!input.Name.IsNullOrEmpty(),
                p => p.Name.Contains(input.Name))
            .Where(!input.RealName.IsNullOrEmpty(),
                p => p.RealName.Contains(input.RealName))
            .OrderByDescending(p=> p.Sort)
            .ThenByDescending(p=> p.CreatedTime)
            .ProjectToType<GetRolePageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return pageRoles;
    }

    public Task<ICollection<string>> GetPermissionsAsync(long roleId)
    {
        return _roleManager.GetPermissionsAsync(roleId);
    }

    public Task<bool> CheckHasMenusAsync(long[] menuIds)
    {
        return _roleManager.CheckHasMenusAsync(menuIds);
    }

    private async Task UpdateRoleByInput(IdentityRole role, RoleDtoBase input)
    {
        role.IsDefault = input.IsDefault;
        role.IsPublic = input.IsPublic;
        role.Sort = input.Sort;
        role.Status = input.Status;
        role.Remark = input.Remark;
        (await _roleManager.SetRoleNameAsync(role, input.Name)).CheckErrors();
        (await _roleManager.SetRoleRealNameAsync(role, input.RealName)).CheckErrors();
    }

    private async Task RemoveUserRoleCacheAsync(long roleId)
    {
        var userRoles = await _userRoleRepository.AsQueryable(false).Where(p => p.RoleId == roleId).ToArrayAsync();
        foreach (var userRole in userRoles)
        {
            await _distributedCache.RemoveAsync(typeof(GetUserOutput), $"id:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(GetUserRoleOutput), $"roles:userId:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(ICollection<long>), $"roleIds:userId:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(GetCurrentUserDataRange), $"CurrentUserDataRange:userId:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(ICollection<GetCurrentUserMenuOutput>), $"CurrentUserMenus:userId:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(string[]), $"CurrentUserPermissioncodes:userId:{userRole.UserId}");
            await _distributedCache.RemoveMatchKeyAsync(typeof(bool), $"permissionName:*:userId:{userRole.UserId}");
            await _distributedCache.RemoveMatchKeyAsync(typeof(bool), $"roleName:*:userId:{userRole.UserId}");
        }
        
    }

    public async Task<ICollection<GetRoleOutput>> GetListAsync(string realName,string name)
    {
        return await _roleManager.RoleRepository.AsQueryable(false)
              .Where(!realName.IsNullOrEmpty(), p => p.RealName.Contains(realName))
              .Where(!name.IsNullOrEmpty(), p => p.Name.Contains(name))
              .ProjectToType<GetRoleOutput>()
              .ToListAsync();
    }
}