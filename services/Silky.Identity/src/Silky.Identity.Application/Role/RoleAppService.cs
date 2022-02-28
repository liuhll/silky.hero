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
using Silky.Core;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.Caching;
using Silky.EntityFrameworkCore.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.EntityFrameworkCore;
using Silky.Hero.Common.Enums;
using Silky.Hero.Common.Session;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Identity.Domain.Shared;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Transaction.Tcc;
using IdentityRole = Silky.Identity.Domain.IdentityRole;

namespace Silky.Identity.Application.Role;

public class RoleAppService : IRoleAppService
{
    private readonly IdentityRoleManager _roleManager;
    private readonly IRepository<IdentityUserRole> _userRoleRepository;
    private readonly ISession _session;
    private readonly IDistributedCache _distributedCache;
    private readonly IOrganizationAppService _organizationAppService;

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

    public Task<GetRoleDetailOutput> GetDetailAsync(long id)
    {
        return _roleManager.GetRoleOutputByIdAsync(id);
    }

    public async Task<GetRoleOutput> GetAsync(long id)
    {
        return (await _roleManager.GetByIdAsync(id)).Adapt<GetRoleOutput>();
    }

    public async Task DeleteAsync(long id)
    {
        var role = await _roleManager.GetByIdAsync(id);
        (await _roleManager.DeleteAsync(role)).CheckErrors();
        await RemoveUserRoleCacheAsync(role.Id);
    }

    public async Task<bool> CheckAsync(CheckRoleInput input)
    {
        var exsit = false;
        switch (input.RoleNameType)
        {
            case RoleNameType.Name:
                exsit = await _roleManager.RoleRepository.AnyAsync(p => p.NormalizedName == input.Name.ToUpper() && p.Id != input.Id,
                    false);
                break;
            case RoleNameType.RealName:
                exsit = await _roleManager.RoleRepository.AnyAsync(p => p.RealName == input.Name && p.Id != input.Id,
                    false);
                break;
        }

        return exsit;
    }

    [UnitOfWork]
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

    public async Task<ICollection<GetRoleOutput>> GetListAsync(string realName, string name, Status? status)
    {
        return await _roleManager.RoleRepository
            .AsQueryable(false)
            .Where(!name.IsNullOrEmpty(), p => p.Name.Contains(name))
            .Where(!realName.IsNullOrEmpty(), p => p.RealName.Contains(realName))
            .Where(status.HasValue, p => p.Status == status)
            .OrderByDescending(p => p.Sort)
            .ThenByDescending(p => p.CreatedTime)
            .ProjectToType<GetRoleOutput>()
            .ToListAsync();
    }

    public async Task<PagedList<GetRolePageOutput>> GetPageAsync(GetRolePageInput input)
    {
        var pageRoles = await _roleManager.RoleRepository
            .AsQueryable(false)
            .Where(!input.Name.IsNullOrEmpty(),
                p => p.Name.Contains(input.Name))
            .Where(!input.RealName.IsNullOrEmpty(),
                p => p.RealName.Contains(input.RealName))
            .OrderByDescending(p => p.Sort)
            .ThenByDescending(p => p.CreatedTime)
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

    [TccTransaction(ConfirmMethod = "CreateConfirmSuperRoleAsync", CancelMethod = "CreateCancelSuperRoleAsync")]
    public async Task<string> CreateSuperRoleAsync(long tenantId, string superRoleName, string superRealName)
    {
        UpdateCurrentTenantId(tenantId);
        var role = new IdentityRole(superRoleName, superRealName, tenantId);
        role.Status = Status.Invalid;
        role.SetDataRange(DataRange.Whole);
        var menuIds = await _roleManager.MenuAppService.GetAllMenuIdsAsync(true);
        role.AddMenus(menuIds);
        (await _roleManager.CreateAsync(role)).CheckErrors();
        return role.Name;
    }

    public async Task<ICollection<GetRoleOutput>> GetAllocationOrganizationRoleListAsync()
    {
        return await _roleManager.RoleRepository
            .AsQueryable(false)
            .ProjectToType<GetRoleOutput>().ToListAsync();
    }

    public async Task<ICollection<GetRoleOutput>> GetPublicRoleListAsync()
    {
        return await _roleManager.RoleRepository
            .AsQueryable(false)
            .Where(p => p.IsPublic)
            .ProjectToType<GetRoleOutput>().ToListAsync();
    }

    public async Task<string> CreateConfirmSuperRoleAsync(long tenantId, string superRoleName, string superRealName)
    {
        UpdateCurrentTenantId(tenantId);
        var role = await _roleManager.RoleRepository.FirstOrDefaultAsync(p => p.Name == superRoleName);
        role.Status = Status.Valid;
        (await _roleManager.UpdateAsync(role)).CheckErrors();
        return role.Name;
    }

    [UnitOfWork]
    public async Task<string> CreateCancelSuperRoleAsync(long tenantId, string superRoleName, string superRealName)
    {
        UpdateCurrentTenantId(tenantId);
        var role = await _roleManager.RoleRepository.FirstOrDefaultAsync(p => p.Name == superRoleName);
        if (role != null)
        {
            (await _roleManager.DeleteAsync(role)).CheckErrors();
            return role.Name;
        }

        return null;
    }

    private void UpdateCurrentTenantId(long tenantId)
    {
        var currentTenantId = EngineContext.Current.Resolve<ICurrentTenantId>();
        currentTenantId.SetTenantId(tenantId);
    }

    private async Task UpdateRoleByInput(IdentityRole role, RoleDtoBase input)
    {
        role.IsDefault = input.IsDefault;
        role.IsPublic = input.IsPublic;
        role.IsStatic = input.IsStatic;
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
            await _distributedCache.RemoveAsync(typeof(GetCurrentUserDataRange),
                $"CurrentUserDataRange:userId:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(ICollection<GetCurrentUserMenuOutput>),
                $"CurrentUserMenus:userId:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(string[]),
                $"CurrentUserPermissionCodes:userId:{userRole.UserId}");
            await _distributedCache.RemoveAsync(typeof(ICollection<GetOrganizationTreeOutput>), $"tree:userId:{userRole.UserId}");
            await _distributedCache.RemoveMatchKeyAsync(typeof(bool), $"permissionName:*:userId:{userRole.UserId}");
            await _distributedCache.RemoveMatchKeyAsync(typeof(bool), $"roleName:*:userId:{userRole.UserId}");
            await _distributedCache.RemoveMatchKeyAsync(typeof(GetOrganizationOutput), "id:*");
        }
    }
}