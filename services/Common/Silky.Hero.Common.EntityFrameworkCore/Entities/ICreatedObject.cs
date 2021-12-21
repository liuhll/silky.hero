namespace Silky.Hero.Common.EntityFrameworkCore.Entities;
public interface ICreatedObject : IHasCreatedTime
{
    public long? CreatedBy { get; set; }

    
}