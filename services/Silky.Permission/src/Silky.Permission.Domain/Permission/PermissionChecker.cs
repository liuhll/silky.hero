using System.Threading.Tasks;
using Silky.Core.DependencyInjection;

namespace Silky.Permission.Domain;

public class PermissionChecker : IPermissionChecker, IScopedDependency
{
    public Task<bool> IsGrantedByPermissionAsync(string permissionName)
    {
        return Task.FromResult(true);
    }

    public Task<bool> IsGrantedByRoleAsync(string permissionName)
    {
        return Task.FromResult(true);
    }
}