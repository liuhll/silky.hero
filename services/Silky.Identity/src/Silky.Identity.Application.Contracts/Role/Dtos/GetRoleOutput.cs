using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class GetRoleOutput : RoleDtoBase
{
    public long Id { get; set; }
    
    /// <summary>
    /// 数据权限范围
    /// </summary>
    public DataRange DataRange { get; set; }
}