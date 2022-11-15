namespace Silky.Position.Application.Contracts.Position.Dtos;

public class UpdatePositionInput : PositionDtoBase
{
    /// <summary>
    /// 职位主键
    /// </summary>
    public long Id { get; set; }
}