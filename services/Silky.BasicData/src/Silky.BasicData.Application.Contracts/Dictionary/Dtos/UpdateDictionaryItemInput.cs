using Silky.BasicData.Domain.Shared.Dictionary.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Runtime.Server;

namespace Silky.BasicData.Application.Contracts.Dictionary.Dtos;

public class UpdateDictionaryItemInput : DictionaryItemDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }
}