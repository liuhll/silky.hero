using System.ComponentModel.DataAnnotations;
using Silky.Hero.Common.Enums;

namespace Silky.Permission.Application.Contracts.Menu.Dtos;

public class CreateOrUpdateButtonInput
{
    /// <summary>
    /// 主键id
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    [Required(ErrorMessage = "菜单名称不允许为空")]
    public string Name { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    [Required(ErrorMessage = "权限标识不允许为空")]
    public string PermissionCode { get; set; }

    /// <summary>
    /// 上级菜单id
    /// </summary>
    public long ParentId { get; set; }


    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }


    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }
}