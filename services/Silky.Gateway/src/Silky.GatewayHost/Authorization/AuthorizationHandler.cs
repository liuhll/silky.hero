using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Silky.Http.Identity.Authorization.Handlers;
using Silky.Http.Identity.Authorization.Requirements;
using Silky.Permission.Application.Contracts.Permission;
using Silky.Rpc.Extensions;

namespace Silky.GatewayHost.Authorization;

public class AuthorizationHandler : SilkyAuthorizationHandlerBase
{
    private readonly IPermissionAppService _permissionAppService;

    public AuthorizationHandler(IPermissionAppService permissionAppService)
    {
        _permissionAppService = permissionAppService;
    }

    protected override async Task<bool> PolicyPipelineAsync(AuthorizationHandlerContext context,
        HttpContext httpContext,
        IAuthorizationRequirement requirement)
    {
        if (requirement is PermissionRequirement permissionRequirement)
        {
            return await _permissionAppService.CheckPermissionAsync(permissionRequirement.PermissionName);
        }

        return true;
    }

    protected override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, HttpContext httpContext)
    {
        var serviceEntry = httpContext.GetServiceEntry();
        var roles = serviceEntry
            .AuthorizeData
            .Where(p => !p.Roles.IsNullOrEmpty())
            .SelectMany(p => p.Roles?.Split(","))
            .ToList();
        foreach (var role in roles)
        {
            if (!await _permissionAppService.CheckRoleAsync(role))
            {
                return false;
            }
        }

        return true;
    }
}