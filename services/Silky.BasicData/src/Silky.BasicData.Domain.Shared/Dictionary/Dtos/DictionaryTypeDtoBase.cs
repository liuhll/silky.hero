using System.ComponentModel.DataAnnotations;

namespace Silky.BasicData.Domain.Shared.Dictionary.Dtos;

public abstract class DictionaryTypeDtoBase
{
    /// <summary>
    /// 字段名称
    /// </summary>
    [Required(ErrorMessage = "字典名称不允许为空")]
    public string Name { get; set; }

    /// <summary>
    /// 字典编码(唯一标识)
    /// </summary>
    [Required(ErrorMessage = "字典编码不允许为空")]
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