using Silky.Hero.Common.Dtos;

namespace Silky.BasicData.Application.Contracts.Dictionary.Dtos;

public class GetDictionaryTypePageInput : PageDtoBase
{
    /// <summary>
    /// 字典名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 字典编码(唯一标识)
    /// </summary>
    public string Code { get; set; }
}