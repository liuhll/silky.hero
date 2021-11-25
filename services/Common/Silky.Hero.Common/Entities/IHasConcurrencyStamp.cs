namespace Silky.Hero.Common.Entities;

public interface IHasConcurrencyStamp
{
    string ConcurrencyStamp { get; set; }
}