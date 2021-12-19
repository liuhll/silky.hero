using Silky.EntityFrameworkCore.Entities;

namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public abstract class AuditedEntity : Entity<long>, IAuditedObject, IHasTenantObject
{
    protected AuditedEntity()
    {
    }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public long? TenantId { get; set; }
}