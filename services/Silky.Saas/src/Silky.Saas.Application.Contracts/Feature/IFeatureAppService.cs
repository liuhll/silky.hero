using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;
using Silky.Saas.Application.Contracts.Feature.Dtos;

namespace Silky.Saas.Application.Contracts.Feature;

/// <summary>
/// 功能特性服务
/// </summary>
[ServiceRoute]
[Authorize]
public interface IFeatureAppService
{
    /// <summary>
    /// 获取功能特性列表
    /// </summary>
    /// <returns></returns>
    [GetCachingIntercept("GetFeatures", IgnoreMultiTenancy = true)]
    Task<ICollection<GetFeatureCatalogOutput>> GetFeaturesAsync();
}