using System.Linq;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.Core.Runtime.Session;

namespace Silky.Permission.Domain;

public class PermissionChecker : IPermissionChecker, IScopedDependency
{
    private readonly IPermissionManager _permissionManager;
    private readonly ISession _session;

    public PermissionChecker(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
        _session = NullSession.Instance;
    }

    public async Task<bool> IsGrantedByPermissionAsync(string permissionName)
    {
        var userRoleIds = await _permissionManager.GetUserValidRoleIdsAsync(long.Parse(_session.UserId.ToString()));
        if (!userRoleIds.Any())
        {
            return false;
        }

        foreach (var userRoleId in userRoleIds)
        {
            if (await IsGrantedAsync(userRoleId, permissionName))
            {
                return true;
            }
        }

        return false;
    }

    private async Task<bool> IsGrantedAsync(long roleId, string permissionName)
    {
        var rolePermissions = await _permissionManager.GetRolePermissionsAsync(roleId);
        return rolePermissions.Any(p => p == permissionName);
    }

    public async Task<bool> IsGrantedByRoleAsync(string roleName)
    {
        var userRoleNames = await _permissionManager.GetUserValidRoleNamesAsync(long.Parse(_session.UserId.ToString()));
        return userRoleNames.Any(p => p == roleName);
    }
}