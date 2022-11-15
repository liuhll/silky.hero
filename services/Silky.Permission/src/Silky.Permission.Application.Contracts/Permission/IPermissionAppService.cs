using System.Threading.Tasks;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.Permission.Application.Contracts.Permission;

[ServiceRoute]
[Authorize]
public interface IPermissionAppService
{
    [ProhibitExtranet]
    [GetCachingIntercept("permissionName:{permissionName}", OnlyCurrentUserData = true)]
    Task<bool> CheckPermissionAsync(string permissionName);

    [ProhibitExtranet]
    [GetCachingIntercept("roleName:{roleName}", OnlyCurrentUserData = true)]
    Task<bool> CheckRoleAsync(string roleName);
}