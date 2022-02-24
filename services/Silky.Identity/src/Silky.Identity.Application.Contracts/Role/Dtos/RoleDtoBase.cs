using System.ComponentModel.DataAnnotations;
using Silky.Hero.Common.Enums;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public abstract class RoleDtoBase
{
    /// <summary>
    /// 角色标识
    /// </summary>
    [Required(ErrorMessage = "角色标识不允许为空")]
    [RegularExpression(RegularExpressionConsts.RoleName,ErrorMessage = "角色标识格式不正确")]
    public string Name { get; set; }

    /// <summary>
    /// 角色真实名称
    /// </summary>
    [Required(ErrorMessage = "角色真实名称不允许为空")]
    public string RealName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
    
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 是否默认角色
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// 是否公共角色
    /// </summary>
    public bool IsPublic { get; set; }
    
    /// <summary>
    /// 是否静态角色
    /// </summary>
    public bool IsStatic { get; set; }
}