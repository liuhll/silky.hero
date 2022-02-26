using System.ComponentModel.DataAnnotations;
using Silky.Hero.Common.Enums;

namespace Silky.Position.Application.Contracts.Position.Dtos;

public abstract class PositionDtoBase
{
    
    /// <summary>
    /// 职位名称
    /// </summary>
    [Required(ErrorMessage = "职位名称不允许为空")]
    public string Name { get; set; }
    
    /// <summary>
    /// 排序字段
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    
    /// <summary>
    /// 是否静态职位
    /// </summary>
    public bool IsStatic { get; set; }
    
    /// <summary>
    /// 是否公共职位
    /// </summary>
    public bool IsPublic { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }
}