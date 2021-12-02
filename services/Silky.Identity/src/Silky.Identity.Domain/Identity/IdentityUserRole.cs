using System;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityUserRole : AuditedEntity
{
    public long UserId { get; set; }

    public long RoleId { get; set; }
    
    protected IdentityUserRole()
    {
    } 
    
    protected internal IdentityUserRole(long userId, long roleId, Guid? tenantId)
    {
        UserId = userId;
        RoleId = roleId;
        TenantId = tenantId;
    }
    
}