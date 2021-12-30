using Silky.Rpc.CachingInterceptor;

namespace Silky.Permission.Application.Contracts.Menu.Dtos;

public class UpdateMenuInput : MenuDtoBase
{
    /// <summary>
    /// 主键id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }

   
}