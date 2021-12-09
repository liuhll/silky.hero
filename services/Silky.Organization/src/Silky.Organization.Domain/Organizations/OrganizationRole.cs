using System;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Organization.Domain;

public class OrganizationRole : AuditedEntity
{
    public long RoleId { get; protected set; }


    public long OrganizationId { get; protected set; }


    public OrganizationRole()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationRole"/> class.
    /// </summary>
    /// <param name="tenantId">TenantId</param>
    /// <param name="roleId">Id of the User.</param>
    /// <param name="organizationId">Id of the <see cref="organizationId"/>.</param>
    public OrganizationRole(long roleId, long organizationId, Guid? tenantId = null)
    {
        RoleId = roleId;
        OrganizationId = organizationId;
        TenantId = tenantId;
    }
}