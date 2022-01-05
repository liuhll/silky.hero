using System.ComponentModel.DataAnnotations;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class UpdateRoleMenuInput
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色菜单
    /// </summary>
    [Required(ErrorMessage = "菜单id不允许为空")]
    public long[] MenuIds { get; set; }
}