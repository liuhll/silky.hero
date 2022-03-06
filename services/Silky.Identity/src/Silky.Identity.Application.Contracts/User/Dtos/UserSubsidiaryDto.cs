namespace Silky.Identity.Application.Contracts.User.Dtos;

public class UserSubsidiaryDto
{
    /// <summary>
    /// 组织机构Id
    /// </summary>
    public long OrganizationId { get; set; }

    /// <summary>
    /// 岗位id
    /// </summary>
    public long PositionId { get; set; }

    /// <summary>
    /// 是否是部门负责人
    /// </summary>
    public bool IsLeader { get; set; }
}