using Silky.Rpc.CachingInterceptor;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class UpdateTenantInput : TenantDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }
}