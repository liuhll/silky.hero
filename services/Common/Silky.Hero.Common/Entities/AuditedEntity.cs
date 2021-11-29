using Silky.EntityFrameworkCore.Entities;

namespace Silky.Hero.Common.Entities;

public abstract class AuditedEntity : Entity<long>, IAuditedObject
{
    protected AuditedEntity()
    {
    }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }
}