using Silky.Hero.Common.Dtos;
using Silky.Hero.Common.Enums;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetUserPageInput : PageDtoBase
{
    public string UserName { get; set; }

    public string RealName { get; set; }

    public string MobilePhone { get; set; }

    public string TelPhone { get; set; }

    public string Email { get; set; }
    
    public Status? Status { get; set; }
    
    public bool? IsLockout { get; set; }

    public Sex? Sex { get; set; }

    public string JobNumber { get; set; }

    public long[] OrganizationIds { get; set; }

    public long[] PositionIds { get; set; }
    
    public long[] RoleIds { get; set; }
}