using Silky.Hero.Common.Dtos;

namespace Silky.BasicData.Application.Contracts.Dictionary.Dtos;

public class GetDictionaryItemPageInput : PageDtoBase
{
    
    /// <summary>
    /// 字典项的值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 字典编码(唯一标识)
    /// </summary>
    public string Code { get; set; }
}