using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class UserSubsidiary : AuditedEntity
{
    public long OrganizationId { get; set; }

    public long PositionId { get; set; }

    public long UserId { get; set; }

    public bool IsLeader { get; set; }

    public UserSubsidiary()
    {
    }

    public UserSubsidiary(long userId, long organizationId, long positionId, bool isLeader, long? tenantId)
    {
        UserId = userId;
        OrganizationId = organizationId;
        PositionId = positionId;
        IsLeader = isLeader;
        TenantId = tenantId;
    }
}