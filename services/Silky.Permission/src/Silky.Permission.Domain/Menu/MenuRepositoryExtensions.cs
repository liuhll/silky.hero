using System.Linq;
using System.Threading.Tasks;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Permission.Domain.Shared.Menu;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Application.Contracts.Edition.Dtos;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Permission.Domain.Menu;

public static class MenuRepositoryExtensions
{
    public static async Task<IQueryable<Menu>> GetCurrentTenantMenus(this IRepository<Menu> menuRepository)
    {
        var enabledAuditingLog = await GetEditionFeatureAsync(FeatureCode.EnabledAuditingLog);
        var enabledMenuManage = await GetEditionFeatureAsync(FeatureCode.EnabledMenuManage);
        var enabledSaasManage = await GetEditionFeatureAsync(FeatureCode.EnabledSaasManage);

        return menuRepository.AsQueryable(false)
                .Where(enabledAuditingLog?.FeatureValue.To<bool>() == false,
                    p => p.Name != MenuConsts.AuditLogMenuName && p.Name != MenuConsts.LogMangeMenuName)
                .Where(enabledMenuManage?.FeatureValue.To<bool>() == false, p => p.Name != MenuConsts.MenuMenuName)
                .Where(enabledSaasManage?.FeatureValue.To<bool>() == false, p => p.Name != MenuConsts.SaasMenuName)
            ;
    }

    private static async Task<GetEditionFeatureOutput> GetEditionFeatureAsync(string featureCode)
    {
        var editionAppService = EngineContext.Current.Resolve<IEditionAppService>();
        var editionFeatureOutput = await editionAppService.GetEditionFeatureAsync(featureCode);
        return editionFeatureOutput;
    }
}