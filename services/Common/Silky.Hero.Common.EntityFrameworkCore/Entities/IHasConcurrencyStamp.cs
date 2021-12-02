namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface IHasConcurrencyStamp
{
    string ConcurrencyStamp { get; set; }
}