using System.Collections.Generic;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class GetRoleOutput : RoleDtoBase
{
    
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色所拥有的菜单权限
    /// </summary>
    public ICollection<GetRoleMenuTreeOutput> Menus { get; set; }

    /// <summary>
    /// 数据权限范围
    /// </summary>
    public DataRange DataRange { get; set; }

    /// <summary>
    /// 用户所有的数据权限范围
    /// </summary>
    public ICollection<GetCustomOrganizationOutput> CustomOrganizationDataRanges { get; set; }

}