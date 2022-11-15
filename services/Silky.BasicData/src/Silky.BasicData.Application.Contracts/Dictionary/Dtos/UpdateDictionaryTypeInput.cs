using Silky.BasicData.Domain.Shared.Dictionary.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Runtime.Server;

namespace Silky.BasicData.Application.Contracts.Dictionary.Dtos;

public class UpdateDictionaryTypeInput : DictionaryTypeDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
}