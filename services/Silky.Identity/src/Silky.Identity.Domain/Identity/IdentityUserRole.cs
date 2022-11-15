using Silky.EntityFrameworkCore.Extras.Entities;

namespace Silky.Identity.Domain;

public class IdentityUserRole : AuditedEntity
{
    public long UserId { get; set; }

    public long RoleId { get; set; }
    
    public IdentityUserRole()
    {
    } 
    
    protected internal IdentityUserRole(long userId, long roleId, long? tenantId)
    {
        UserId = userId;
        RoleId = roleId;
        TenantId = tenantId;
    }
    
}