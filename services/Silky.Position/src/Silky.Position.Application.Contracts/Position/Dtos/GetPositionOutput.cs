using System;

namespace Silky.Position.Application.Contracts.Position.Dtos;

public class GetPositionOutput : PositionDtoBase
{
    /// <summary>
    /// 职位主键
    /// </summary>
    public long Id { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
}