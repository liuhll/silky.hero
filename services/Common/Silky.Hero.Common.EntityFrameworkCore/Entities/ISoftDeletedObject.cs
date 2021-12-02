namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface ISoftDeletedObject
{
    bool IsDeleted { get; set; }

    long? DeletedBy { get; set; }

    DateTimeOffset? DeletedTime { get; set; }
}