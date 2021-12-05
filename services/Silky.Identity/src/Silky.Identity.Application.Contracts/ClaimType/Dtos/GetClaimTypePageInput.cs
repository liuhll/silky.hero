using Silky.Hero.Common.Dtos;

namespace Silky.Identity.Application.Contracts.ClaimType.Dtos;

public class GetClaimTypePageInput : PageDtoBase
{
    /// <summary>
    /// 声明名称
    /// </summary>
    public string Name { get; set; }
}