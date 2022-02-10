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
    [GetCachingIntercept("permissionName:{0}", OnlyCurrentUserData = true)]
    Task<bool> CheckPermissionAsync([CacheKey(0)]string permissionName);

    [ProhibitExtranet]
    [GetCachingIntercept("roleName:{0}", OnlyCurrentUserData = true)]
    Task<bool> CheckRoleAsync([CacheKey(0)]string roleName);
}