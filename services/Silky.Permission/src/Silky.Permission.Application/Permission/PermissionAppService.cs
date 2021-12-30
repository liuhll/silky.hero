using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Permission.Application.Contracts.Permission;
using Silky.Permission.Application.Contracts.Permission.Dtos;
using Silky.Permission.Domain;

namespace Silky.Permission.Application.Permission;

public class PermissionAppService : IPermissionAppService
{
    private readonly IPermissionManager _permissionManager;

    public PermissionAppService(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public Task<bool> CheckAsync(AuthorizeInput input)
    {
        return _permissionManager.CheckAsync(input.AuthorizeDatas);
    }
}