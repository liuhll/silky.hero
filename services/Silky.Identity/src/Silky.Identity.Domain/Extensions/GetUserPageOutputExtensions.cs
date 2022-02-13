using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Core;
using Silky.Identity.Application.Contracts.Role;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Position.Application.Contracts.Position;

namespace Silky.Identity.Domain.Extensions;

public static class GetUserPageOutputExtensions
{
    public static async Task SetUserSubsidiaries(this IEnumerable<GetUserOutput> userOutputs)
    {
        var positionAppService = EngineContext.Current.Resolve<IPositionAppService>();
        var organizationAppService = EngineContext.Current.Resolve<IOrganizationAppService>();
        foreach (var userPageOutput in userOutputs)
        {
            foreach (var userSubsidiaryOutput in userPageOutput.UserSubsidiaries)
            {
                userSubsidiaryOutput.OrganizationName =
                    (await organizationAppService.GetAsync(userSubsidiaryOutput.OrganizationId))?.Name;
                userSubsidiaryOutput.PositionName =
                    (await positionAppService.GetAsync(userSubsidiaryOutput.PositionId))?.Name;
            }
        }
    }
    
    public static async Task SetUserSubsidiaries(this GetUserOutput userOutput)
    {
        var positionAppService = EngineContext.Current.Resolve<IPositionAppService>();
        var organizationAppService = EngineContext.Current.Resolve<IOrganizationAppService>();
        foreach (var userSubsidiaryOutput in userOutput.UserSubsidiaries)
        {
            userSubsidiaryOutput.OrganizationName =
                (await organizationAppService.GetAsync(userSubsidiaryOutput.OrganizationId))?.Name;
            userSubsidiaryOutput.PositionName =
                (await positionAppService.GetAsync(userSubsidiaryOutput.PositionId))?.Name;
        }
    }

    public static async Task SetUserRoles(this IEnumerable<GetUserOutput> userOutputs) 
    {
        var roleAppService = EngineContext.Current.Resolve<IRoleAppService>();
        foreach (var userPageOutput in userOutputs) 
        {
            foreach (var userRolePageOutput in userPageOutput.Roles)
            {
                var roleInfo = (await roleAppService.GetAsync(userRolePageOutput.RoleId));
                userRolePageOutput.Name = roleInfo.Name;
                userRolePageOutput.RealName = roleInfo.RealName;
                userRolePageOutput.Status = roleInfo.Status;
            }
        }
    }
    
    public static async Task SetUserRoles(this GetUserOutput userOutput) 
    {
        var roleAppService = EngineContext.Current.Resolve<IRoleAppService>();
        foreach (var userRolePageOutput in userOutput.Roles)
        {
            var roleInfo = (await roleAppService.GetAsync(userRolePageOutput.RoleId));
            userRolePageOutput.Name = roleInfo.Name;
            userRolePageOutput.RealName = roleInfo.RealName;
            userRolePageOutput.Status = roleInfo.Status;
        }
    }
}