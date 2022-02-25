using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Silky.Core;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain;

public static class GetOrganizationOutputExtensions
{
    public static async Task SetRoleInfo(this GetOrganizationOutput organizationOutput,
        ICollection<GetRoleOutput> publicRoles)
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

        foreach (var publicRole in publicRoles)
        {
            if (organizationOutput.OrganizationRoles.Any(p => p.RoleId == publicRole.Id))
            {
                continue;
            }

            var roleOutput = new GetOrganizationRoleOutput()
            {
                RoleId = publicRole.Id,
                Name = publicRole.Name,
                RealName = publicRole.RealName,
                Status = publicRole.Status,
                IsDefault = publicRole.IsDefault,
                IsStatic = publicRole.IsStatic,
                IsPublic = publicRole.IsPublic,
            };
            organizationOutput.OrganizationRoles.Add(roleOutput);
        }
    }
}