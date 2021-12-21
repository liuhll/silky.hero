namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface IHasCreatedTime
{
    public DateTimeOffset CreatedTime { get; set; }
}