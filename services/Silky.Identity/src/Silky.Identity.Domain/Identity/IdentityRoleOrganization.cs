using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityRoleOrganization : AuditedEntity
{
    public IdentityRoleOrganization()
    {
    }

    public IdentityRoleOrganization(long roleId, long organizationId)
    {
        RoleId = roleId;
        OrganizationId = organizationId;
    }

    public long RoleId { get; set; }

    public long OrganizationId { get; set; }
}