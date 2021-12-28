using System.ComponentModel;

namespace Silky.Permission.Domain.Shared.Menu;

public enum MenuType
{
    [Description("目录")]
    Catalog,

    [Description("菜单")]
    Menu,

    [Description("按钮")]
    Button
}