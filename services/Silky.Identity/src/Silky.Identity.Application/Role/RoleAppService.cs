using System.Threading.Tasks;
using Mapster;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Domain;
using Silky.Rpc.Runtime.Server;
using Microsoft.AspNetCore.Identity;
using IdentityRole = Silky.Identity.Domain.IdentityRole;

namespace Silky.Identity.Application.Role;

public class RoleAppService : IRoleAppService
{
    private readonly IdentityRoleManager _roleManager;
    private readonly ISession _session;

    public RoleAppService(IdentityRoleManager roleManager)
    {
        _roleManager = roleManager;
        _session = NullSession.Instance;
    }

    [UnitOfWork]
    public async Task CreateOrUpdateAsync(CreateOrUpdateRoleInput input)
    {
        var role = !input.Id.HasValue
            ? new IdentityRole(input.Name, input.RealName, _session.TenantId)
            : await _roleManager.GetByIdAsync(input.Id.Value);
        await UpdateRoleByInput(role, input);
        if (!input.Id.HasValue)
        {
            (await _roleManager.CreateAsync(role)).CheckErrors();
        }
        else
        {
            (await _roleManager.UpdateAsync(role)).CheckErrors();
        }
    }

    public async Task<GetRoleOutput> GetAsync(long id)
    {
        var role = await _roleManager.GetByIdAsync(id);
        return role.Adapt<GetRoleOutput>();
    }

    private async Task UpdateRoleByInput(IdentityRole role, CreateOrUpdateRoleInput input)
    {
        role.IsDefault = input.IsDefault;
        role.IsPublic = input.IsPublic;
        role.Sort = input.Sort;
        (await _roleManager.SetRoleNameAsync(role, input.Name)).CheckErrors();
        (await _roleManager.SetRoleRealNameAsync(role, input.Name)).CheckErrors();
    }
}