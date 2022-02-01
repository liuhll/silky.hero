using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.User;

namespace Silky.Permission.Domain;

public class PermissionManager : IPermissionManager, IScopedDependency
{
    private readonly IUserAppService _userAppService;
    private readonly IRoleAppService _roleAppService;

    public PermissionManager(IUserAppService userAppService,
        IRoleAppService roleAppService)
    {
        _userAppService = userAppService;
        _roleAppService = roleAppService;
    }

    public async Task<ICollection<string>> GetUserValidRoleNamesAsync(long userId)
    {
        var userRoleOutput = await _userAppService.GetValidRolesAsync(userId);
        return userRoleOutput.RoleNames;
    }

    public async Task<ICollection<long>> GetUserValidRoleIdsAsync(long userId)
    {
        var userRoleIds = await _userAppService.GetValidRoleIdsAsync(userId);
        return userRoleIds;
    }

    public async Task<ICollection<string>> GetRolePermissionsAsync(long roleId)
    {
        var rolePermissions = await _roleAppService.GetPermissionsAsync(roleId);
        return rolePermissions;
    }
}