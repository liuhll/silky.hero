using Silky.EntityFrameworkCore.Entities;

namespace Silky.Hero.Common.Entities;

public abstract class FullAuditedEntity : Entity<long>
{

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public long? DeletedBy { get; set; }

    public DateTimeOffset? DeletedTime { get; set; }

    protected FullAuditedEntity()
    {
    }
    
}