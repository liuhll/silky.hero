using System.Linq;
using System.Threading.Tasks;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.Permission.Domain.Shared.Menu;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Application.Contracts.Edition.Dtos;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Permission.Domain.Menu;

public static class MenuRepositoryExtensions
{
    public static IQueryable<Menu> GetCurrentTenantMenus(this IQueryable<Menu> menuQueryable)
    {
        var enabledAuditingLog = GetEditionFeatureAsync(FeatureCode.EnabledAuditingLog).GetAwaiter().GetResult();
        var enabledMenuManage = GetEditionFeatureAsync(FeatureCode.EnabledMenuManage).GetAwaiter().GetResult();
        var enabledSaasManage = GetEditionFeatureAsync(FeatureCode.EnabledSaasManage).GetAwaiter().GetResult();

        return menuQueryable.Where(enabledAuditingLog?.FeatureValue.To<bool>() == false,
                p => p.Name != MenuConsts.AuditLogMenuName)
            .Where(enabledMenuManage?.FeatureValue.To<bool>() == false, p => p.Name != MenuConsts.MenuMenuName)
            .Where(enabledSaasManage?.FeatureValue.To<bool>() == false, p => p.Name != MenuConsts.SaasMenuName);
    }

    private static async Task<GetEditionFeatureOutput> GetEditionFeatureAsync(string featureCode)
    {
        var editionAppService = EngineContext.Current.Resolve<IEditionAppService>();
        var editionFeatureOutput = await editionAppService.GetEditionFeatureAsync(FeatureCode.EnabledAuditingLog);
        return editionFeatureOutput;
    }
}