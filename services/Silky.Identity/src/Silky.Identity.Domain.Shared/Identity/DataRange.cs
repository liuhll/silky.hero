namespace Silky.Identity.Domain.Shared;

public enum DataRange
{
    /// <summary>
    /// 所有部门数据
    /// </summary>
    Whole,
    
    /// <summary>
    /// 本部门数据
    /// </summary>
    SelfOrganization,
    
    /// <summary>
    /// 本部门及子部门数据
    /// </summary>
    SelfAndChildrenOrganization,
    
    /// <summary>
    /// 自定义数据权限
    /// </summary>
    CustomOrganization
}