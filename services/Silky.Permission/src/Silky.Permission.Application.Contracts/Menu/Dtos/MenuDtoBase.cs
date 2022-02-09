using System.ComponentModel.DataAnnotations;
using Silky.Hero.Common.Enums;
using Silky.Permission.Domain.Shared.Menu;

namespace Silky.Permission.Application.Contracts.Menu.Dtos;

public abstract class MenuDtoBase
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    [Required(ErrorMessage = "菜单名称不允许为空")]
    public string Name { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string PermissionCode { get; set; }

    /// <summary>
    /// 上级菜单id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 前端路由
    /// </summary>
    public string RoutePath { get; set; }

    /// <summary>
    /// 组件
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// 是否外链
    /// </summary>
    public bool? ExternalLink { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool? Display { get; set; }

    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool? KeepAlive { get; set; }
    
    /// <summary>
    /// 是否隐藏面包屑导航 
    /// </summary>
    public bool? HideBreadcrumb { get; set; }
    
    /// <summary>
    /// 当前活动的菜单页
    /// </summary>
    public string CurrentActiveMenu { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public MenuType Type { get; set; }
}