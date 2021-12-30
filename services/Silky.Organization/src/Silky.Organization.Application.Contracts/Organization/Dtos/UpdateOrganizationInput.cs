using Silky.Rpc.CachingInterceptor;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class UpdateOrganizationInput : OrganizationDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }
}