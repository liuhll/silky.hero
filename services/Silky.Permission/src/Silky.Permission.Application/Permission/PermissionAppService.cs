using System.Threading.Tasks;
using Silky.Permission.Application.Contracts.Permission;
using Silky.Permission.Domain;

namespace Silky.Permission.Application.Permission;

public class PermissionAppService : IPermissionAppService
{
    private readonly IPermissionChecker _permissionChecker;

    public PermissionAppService(IPermissionChecker permissionChecker)
    {
        _permissionChecker = permissionChecker;
    }

    public Task<bool> CheckPermissionAsync(string permissionName)
    {
        return _permissionChecker.IsGrantedByPermissionAsync(permissionName);
    }

    public Task<bool> CheckRoleAsync(string roleName)
    {
        return _permissionChecker.IsGrantedByRoleAsync(roleName);;
    }
}