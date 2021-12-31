using System.Threading.Tasks;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

namespace Silky.Permission.Application.Contracts.Permission;

[ServiceRoute]
public interface IPermissionAppService
{
    [Governance(ProhibitExtranet = true)]
    [GetCachingIntercept("permissionName:{0}", OnlyCurrentUserData = true)]
    Task<bool> CheckPermissionAsync([CacheKey(0)]string permissionName);

    [Governance(ProhibitExtranet = true)]
    [GetCachingIntercept("roleName:{0}", OnlyCurrentUserData = true)]
    Task<bool> CheckRoleAsync([CacheKey(0)]string roleName);
}