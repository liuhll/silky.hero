using Silky.Rpc.Runtime.Server;

namespace Silky.Identity.Application.Contracts.ClaimType.Dtos;

public class UpdateClaimTypeInput : ClaimTypeDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }
}