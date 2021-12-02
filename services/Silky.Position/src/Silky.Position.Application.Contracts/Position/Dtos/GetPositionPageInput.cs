using Silky.Hero.Common.Enums;

namespace Silky.Position.Application.Contracts.Position.Dtos;

public class GetPositionPageInput
{
    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 第几页
    /// </summary>
    public int PageIndex { get; set; } = 1;
    
    /// <summary>
    /// 职位名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status? Status { get; set; }
}