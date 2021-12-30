using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Silky.Http.Identity.Authorization.Handlers;
using Silky.Permission.Application.Contracts.Permission;
using Silky.Permission.Application.Contracts.Permission.Dtos;
using Silky.Permission.Domain.Shared.Permission;
using Silky.Rpc.Extensions;

namespace Silky.GatewayHost.Authorization;

public class AuthorizationHandler : SilkyAuthorizationHandlerBase
{
    private readonly IPermissionAppService _permissionAppService;

    public AuthorizationHandler(IPermissionAppService permissionAppService)
    {
        _permissionAppService = permissionAppService;
    }

    protected override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, HttpContext httpContext)
    {
        var serviceEntry = httpContext.GetServiceEntry();
        var authorizeList = serviceEntry
            .AuthorizeData
            .Where(p => !p.Policy.IsNullOrEmpty() && !p.Roles.IsNullOrEmpty())
            .Select(p => new AuthorizeData()
            {
                Policy = p.Policy,
                Roles = p.Roles
            })
            .ToList();
        if (authorizeList.Any())
        {
            return await _permissionAppService.CheckAsync(new AuthorizeInput()
            {
                ServiceEntryId = serviceEntry.Id,
                AuthorizeDatas = authorizeList
            });
        }

        return true;
    }
}