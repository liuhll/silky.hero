using Silky.Rpc.CachingInterceptor;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class UpdateRoleInput : RoleDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }
}