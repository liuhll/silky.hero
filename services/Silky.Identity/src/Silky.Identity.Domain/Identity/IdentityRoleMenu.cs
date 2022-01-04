using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityRoleMenu : FullAuditedEntity
{
    public IdentityRoleMenu()
    {
    }

    public IdentityRoleMenu(long roleId, long menuId)
    {
        RoleId = roleId;
        MenuId = menuId;
    }

    public long RoleId { get; set; }

    public long MenuId { get; set; }
}