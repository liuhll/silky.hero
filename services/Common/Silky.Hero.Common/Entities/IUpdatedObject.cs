namespace Silky.Hero.Common.Entities;

public interface IUpdatedObject
{
    long? UpdatedBy { get; set; }

    DateTimeOffset? UpdatedTime { get; set; }
}