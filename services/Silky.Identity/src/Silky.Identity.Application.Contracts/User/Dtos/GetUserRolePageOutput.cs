using Silky.Hero.Common.Enums;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetUserRolePageOutput
{
    public long RoleId { get; set; }

    public string Name { get; set; }

    public string RealName { get; set; }
    
    public Status Status { get; set; }
}