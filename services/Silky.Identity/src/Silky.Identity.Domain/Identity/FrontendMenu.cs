using System.Collections.Generic;

namespace Silky.Identity.Domain.Identity;

public class FrontendMenu
{
    /// <summary>
    /// 菜单Id
    /// </summary>
    public long Id { get; set; }

    public long? ParentId { get; set; }
    /// <summary>
    /// 路由名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 组件
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    public string Redirect { get; set; }

    /// <summary>
    /// 子路由
    /// </summary>
    public ICollection<FrontendMenu> Children { get; set; }
}
