using System.Threading.Tasks;

namespace Silky.Permission.Domain;

public interface IPermissionChecker
{
    Task<bool> IsGrantedByPermissionAsync(string permissionName);
    
    Task<bool> IsGrantedByRoleAsync(string permissionName);
}