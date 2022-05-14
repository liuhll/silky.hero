using Silky.Identity.Domain.Shared;
using Silky.Rpc.Runtime.Server;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class UpdateRoleDataRangeInput
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [CacheKey(0)]
    public long Id { get; set; }

    /// <summary>
    /// 数据权限访问
    /// </summary>
    public DataRange DataRange { get; set; }

    /// <summary>
    /// 自定义的数据权限范围
    /// </summary>
    public long[] CustomOrganizationIds { get; set; }
}