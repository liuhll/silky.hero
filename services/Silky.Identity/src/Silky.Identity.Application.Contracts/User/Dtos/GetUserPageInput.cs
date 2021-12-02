using Silky.Hero.Common.Dtos;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetUserPageInput : PageDtoBase
{
    public string UserName { get; set; }

    public string RealName { get; set; }

    public string MobilePhone { get; set; }

    public string TelPhone { get; set; }

    public string Email { get; set; }

    public Sex? Sex { get; set; }

    public string JobNumber { get; set; }
}