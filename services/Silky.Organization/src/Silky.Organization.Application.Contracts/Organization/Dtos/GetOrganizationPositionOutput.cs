using Silky.Hero.Common.Enums;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationPositionOutput 
{
    public long PositionId { get; set; }
    
    public string Name { get; set; }
    
    /// <summary>
    /// 是否静态职位
    /// </summary>
    public bool IsStatic { get; set; }
    
    /// <summary>
    /// 是否公共职位
    /// </summary>
    public bool IsPublic { get; set; }
    
    /// <summary>
    /// 状态
    /// </summary>
    public Status Status { get; set; }

   
}