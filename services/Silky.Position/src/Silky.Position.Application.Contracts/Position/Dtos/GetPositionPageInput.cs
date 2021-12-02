using Silky.Hero.Common.Dtos;
using Silky.Hero.Common.Enums;

namespace Silky.Position.Application.Contracts.Position.Dtos;

public class GetPositionPageInput : PageDtoBase
{
    /// <summary>
    /// 职位名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public Status? Status { get; set; }
}