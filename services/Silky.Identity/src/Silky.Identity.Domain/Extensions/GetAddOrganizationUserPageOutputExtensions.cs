using Silky.Core;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Position.Application.Contracts.Position;
using System.Linq;
using System.Threading.Tasks;

namespace Silky.Identity.Domain.Extensions;

public static class GetAddOrganizationUserPageOutputExtensions
{
    public static async Task SetPositionInfo(this GetAddOrganizationUserPageOutput organizationUserOutput,
        long organizationId)
    {
        var positionAppService = EngineContext.Current.Resolve<IPositionAppService>();
        var userOrganizationPosition =
            organizationUserOutput.UserSubsidiaries.FirstOrDefault(p => p.OrganizationId == organizationId);
        if (userOrganizationPosition != null)
        {
            organizationUserOutput.PositionId = userOrganizationPosition.PositionId;
            organizationUserOutput.PositionName =
                (await positionAppService.GetAsync(userOrganizationPosition.PositionId))?.Name;
        }
    }

    public static void SetIsLeader(this GetAddOrganizationUserPageOutput organizationUserOutput, long organizationId)
    {
        organizationUserOutput.IsLeader = organizationUserOutput.UserSubsidiaries
            .FirstOrDefault(p => p.OrganizationId == organizationId)?.IsLeader;
    }
}