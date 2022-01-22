using Silky.Hero.Common.Enums;

namespace Silky.Position.Application.Contracts.Position.Dtos;

public class GetPositionOutput
{
    /// <summary>
    /// 职位主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 排序字段
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