using System.Threading.Tasks;
using Silky.Permission.Application.Contracts.Permission.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

namespace Silky.Permission.Application.Contracts.Permission;

[ServiceRoute]
public interface IPermissionAppService
{
    [Governance(ProhibitExtranet = true)]
    [GetCachingIntercept("permission:serviceentryid:{0}", OnlyCurrentUserData = true)]
    Task<bool> CheckAsync(AuthorizeInput input);
}