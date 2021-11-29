namespace Silky.Hero.Common.Entities;

public interface ICreatedObject
{
    public long? CreatedBy { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
}