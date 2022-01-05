using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityRoleOrganization : FullAuditedEntity
{
    public IdentityRoleOrganization()
    {
    }

    public IdentityRoleOrganization(long roleId, long organizationId,long? tenantId = null)
    {
        RoleId = roleId;
        OrganizationId = organizationId;
        TenantId = tenantId;
    }

    public long RoleId { get; set; }

    public long OrganizationId { get; set; }
}