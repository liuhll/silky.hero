using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public abstract class EditionDtoBase
{
    /// <summary>
    /// 版本名称
    /// </summary>
    [Required(ErrorMessage = "版本名称不允许为空")]
    [MaxLength(50,ErrorMessage = "版本名称不允许超过50个字符")]
    public string Name { get; set; }

    /// <summary>
    /// 版本价格
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

}