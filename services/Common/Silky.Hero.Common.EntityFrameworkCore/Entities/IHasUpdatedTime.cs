namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface IHasUpdatedTime 
{
    DateTimeOffset? UpdatedTime { get; set; }
}