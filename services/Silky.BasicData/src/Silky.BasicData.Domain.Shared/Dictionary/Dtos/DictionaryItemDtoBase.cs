using System.ComponentModel.DataAnnotations;

namespace Silky.BasicData.Domain.Shared.Dictionary.Dtos;

public abstract class DictionaryItemDtoBase
{
    /// <summary>
    /// 字典类型Id
    /// </summary>
    public long DictionaryId { get; set; }

    /// <summary>
    /// 字典项的值
    /// </summary>
    [Required(ErrorMessage = "字典值不允许为空")]
    public string Value { get; set; }

    /// <summary>
    /// 字典编码(唯一标识)
    /// </summary>
    [Required(ErrorMessage = "字典项编码不允许为空")]
    public string Code { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}