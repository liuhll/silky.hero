using Silky.Hero.Common.Dtos;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

/// <summary>
/// 查询角色条件
/// </summary>
public class GetRolePageInput : PageDtoBase
{
    /// <summary>
    /// 角色标识
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 角色真实名称
    /// </summary>
    public string RealName { get; set; }
}