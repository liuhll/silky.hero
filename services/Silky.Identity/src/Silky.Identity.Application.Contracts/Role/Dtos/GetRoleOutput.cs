namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class GetRoleOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色标识
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 角色真实名称
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否默认角色
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// 是否公共角色
    /// </summary>
    public bool IsPublic { get; set; }
}