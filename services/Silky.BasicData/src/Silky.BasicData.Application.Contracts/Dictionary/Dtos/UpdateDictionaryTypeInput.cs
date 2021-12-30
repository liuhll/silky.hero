using Silky.BasicData.Domain.Shared.Dictionary.Dtos;
using Silky.Rpc.CachingInterceptor;

namespace Silky.BasicData.Application.Contracts.Dictionary.Dtos;

public class UpdateDictionaryTypeInput : DictionaryTypeDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }
}