using System.Threading.Tasks;
using Silky.Core;
using Silky.Identity.Application.Contracts.Role;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain;

public static class GetOrganizationOutputExtensions
{
    public static async Task SetRoleInfo(this GetOrganizationOutput organizationOutput)
    {
        var roleAppService = EngineContext.Current.Resolve<IRoleAppService>();
        foreach (var roleOutput in organizationOutput.OrganizationRoles)
        {
            var roleInfo = await roleAppService.GetAsync(roleOutput.RoleId);
            roleOutput.Name = roleInfo.Name;
            roleOutput.RealName = roleInfo.RealName;
            roleOutput.Status = roleInfo.Status;
            roleOutput.IsDefault = roleInfo.IsDefault;
            roleOutput.IsStatic = roleInfo.IsStatic;
            roleOutput.IsPublic = roleInfo.IsPublic;
        }
    }
}