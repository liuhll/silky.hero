using System.Collections.Generic;
using System.Threading.Tasks;

namespace Silky.Permission.Domain;

public interface IPermissionManager
{
    Task<ICollection<string>> GetUserRoleNamesAsync(long userId);
    Task<ICollection<long>> GetUserRoleIdsAsync(long userId);
    Task<ICollection<string>> GetRolePermissionsAsync(long roleId);
}