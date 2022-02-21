using System.ComponentModel.DataAnnotations;
using Silky.Hero.Common.Enums;

namespace Silky.Saas.Application.Contracts.Tenant.Dtos;

public abstract class TenantDtoBase
{
    
    /// <summary>
    /// 租户名称
    /// </summary>
    [Required(ErrorMessage = "租户名称不允许为空")]
    public string Name { get; set; }

    /// <summary>
    /// 状态
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
    /// 版本Id
    /// </summary>
    public long EditionId { get; set; }
    
}