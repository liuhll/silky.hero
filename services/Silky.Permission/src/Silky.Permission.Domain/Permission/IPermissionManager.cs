using System.Collections.Generic;
using System.Threading.Tasks;

namespace Silky.Permission.Domain;

public interface IPermissionManager
{
    Task<ICollection<string>> GetUserValidRoleNamesAsync(long userId);
    Task<ICollection<long>> GetUserValidRoleIdsAsync(long userId);
    Task<ICollection<string>> GetRolePermissionsAsync(long roleId);
}