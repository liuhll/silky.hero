using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Silky.Core;
using Silky.Core.Runtime.Session;
using Silky.Hero.Common.Session;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Position.Application.Contracts.Position;
using Silky.Position.Application.Contracts.Position.Dtos;

namespace Silky.Organization.Domain;

public static class GetOrganizationOutputExtensions
{
    public static async Task SetRolesInfo(this GetOrganizationOutput organizationOutput,
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

        organizationOutput.OrganizationRoles = organizationOutput.OrganizationRoles.OrderByDescending(p => p.IsPublic).ToList();
    }

    public static async Task SetIsBelong(this GetOrganizationOutput organizationOutput, ISession session)
    {
        var currentUserDataRange = await session.GetCurrentUserDataRangeAsync();
        if (currentUserDataRange.IsAllData)
        {
            organizationOutput.IsBelong = true;
        }
        else
        {
            organizationOutput.IsBelong = currentUserDataRange.OrganizationIds.Any(p => p == organizationOutput.Id);
        }
    }

    public static async Task SetPositionsInfo(this GetOrganizationOutput organizationOutput,
        ICollection<GetPositionOutput> publicPositions)
    {
        var positionAppService = EngineContext.Current.Resolve<IPositionAppService>();
        foreach (var positionOutput in organizationOutput.OrganizationPositions)
        {
            var positionInfo = await positionAppService.GetAsync(positionOutput.PositionId);
            positionOutput.Name = positionInfo.Name;
            positionOutput.Status = positionInfo.Status;
            positionOutput.IsStatic = positionInfo.IsStatic;
            positionOutput.IsPublic = positionInfo.IsPublic;
        }

        foreach (var publicPosition in publicPositions)
        {
            if (organizationOutput.OrganizationPositions.Any(p => p.PositionId == publicPosition.Id))
            {
                continue;
            }

            var positionOutput = new GetOrganizationPositionOutput()
            {
                PositionId = publicPosition.Id,
                Name = publicPosition.Name,
                Status = publicPosition.Status,
                IsStatic = publicPosition.IsStatic,
                IsPublic = publicPosition.IsPublic,
            };
            organizationOutput.OrganizationPositions.Add(positionOutput);
        }
        organizationOutput.OrganizationPositions = organizationOutput.OrganizationPositions.OrderByDescending(p => p.IsPublic).ToList();
    }
}