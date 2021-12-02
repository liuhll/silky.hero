namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface IUpdatedObject
{
    long? UpdatedBy { get; set; }

    DateTimeOffset? UpdatedTime { get; set; }
}