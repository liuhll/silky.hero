using Silky.EntityFrameworkCore.Entities;

namespace Silky.Hero.Common.Entities;

public abstract class FullAuditedEntity : AuditedEntity
{
    
    public bool IsDeleted { get; set; }

    public long? DeletedBy { get; set; }

    public DateTimeOffset? DeletedTime { get; set; }

    protected FullAuditedEntity()
    {
    }
    
}