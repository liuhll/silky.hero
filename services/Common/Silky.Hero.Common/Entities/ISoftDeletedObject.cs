namespace Silky.Hero.Common.Entities;

public interface ISoftDeletedObject
{
    bool IsDeleted { get; set; }

    long? DeletedBy { get; set; }

    DateTimeOffset? DeletedTime { get; set; }
}