using System;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class UserSubsidiary : AuditedEntity
{
    public long OrganizationId { get; set; }

    public long PositionId { get; set; }

    public long UserId { get; set; }

    // public virtual IdentityUser User { get; set; }

    public UserSubsidiary()
    {
    }

    public UserSubsidiary(long userId, long organizationId, long positionId, Guid? tenantId)
    {
        UserId = userId;
        OrganizationId = organizationId;
        PositionId = positionId;
        TenantId = tenantId;
    }
}