using System.Threading.Tasks;
using Silky.Core;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Application.Contracts.Tenant.Dtos;

namespace Silky.Saas.Domain;

public static class GetTenantOutputExtensions
{
    public static async Task SetEditionName(this GetTenantOutput output)
    {
        var editionAppService = EngineContext.Current.Resolve<IEditionAppService>();
        var editionOutput = await editionAppService.GetAsync(output.EditionId);
        output.EditionName = editionOutput.Name;
    }
}