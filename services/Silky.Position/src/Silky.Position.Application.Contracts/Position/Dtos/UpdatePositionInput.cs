using Silky.Rpc.CachingInterceptor;

namespace Silky.Position.Application.Contracts.Position.Dtos;

public class UpdatePositionInput : PositionDtoBase
{
    /// <summary>
    /// 职位主键
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }
}