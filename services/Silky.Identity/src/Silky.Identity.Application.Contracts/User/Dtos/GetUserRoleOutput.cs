using System.Collections.Generic;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetUserRoleOutput
{
    public long UserId { get; set; }

    public ICollection<string> RoleNames { get; set; }
}