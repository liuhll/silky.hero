using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class GetRoleDataRangeOutput
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
    /// 数据权限范围
    /// </summary>
    public DataRange DataRange { get; set; }

    /// <summary>
    /// 自定义的数据权限的组织机构Ids
    /// </summary>
    public long[] CustomOrganizationIds { get; set; }
}