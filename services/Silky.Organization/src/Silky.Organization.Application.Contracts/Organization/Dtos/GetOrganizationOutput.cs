using Silky.Hero.Common.Enums;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 父机构Id
    /// </summary>
    public long? ParentId { get; set; }
    
    /// <summary>
    /// 机构名称
    /// </summary>
    public string Name { get; set; }

    
    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }
}