using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityRoleMenu : FullAuditedEntity
{
    public IdentityRoleMenu()
    {
    }

    public IdentityRoleMenu(long roleId, long menuId, long? tenantId = null)
    {
        RoleId = roleId;
        MenuId = menuId;
        TenantId = tenantId;
    }

    public long RoleId { get; set; }

    public long MenuId { get; set; }
}