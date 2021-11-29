namespace Silky.Hero.Common.Entities;

public abstract class FullAuditedEntity : AuditedEntity, ISoftDeletedObject
{
    public bool IsDeleted { get; set; }

    public long? DeletedBy { get; set; }

    public DateTimeOffset? DeletedTime { get; set; }

    protected FullAuditedEntity()
    {
    }
}