using Silky.Rpc.CachingInterceptor;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class UpdateUserInput : UserDtoBase
{
    /// <summary>
    /// 用户id
    /// </summary>
    [CacheKey(0)] public long Id { get; set; }
}