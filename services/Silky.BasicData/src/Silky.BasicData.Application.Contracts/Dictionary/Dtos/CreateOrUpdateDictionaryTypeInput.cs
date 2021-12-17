using Silky.BasicData.Domain.Shared.Dictionary.Dtos;

namespace Silky.BasicData.Application.Contracts.Dictionary.Dtos;

public class CreateOrUpdateDictionaryTypeInput : DictionaryTypeDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long? Id { get; set; }
}