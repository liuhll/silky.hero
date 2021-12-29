using System.Collections.Generic;

namespace Silky.Permission.Application.Contracts.Menu.Dtos;

public class GetMenuPageOutput : MenuDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 子菜单
    /// </summary>
    public ICollection<GetMenuPageOutput> Children { get; set; }
}