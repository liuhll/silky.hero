namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class GetRoleMenuOutput
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 拥有的角色菜单
    /// </summary>
    public long[] MenuIds { get; set; }
}