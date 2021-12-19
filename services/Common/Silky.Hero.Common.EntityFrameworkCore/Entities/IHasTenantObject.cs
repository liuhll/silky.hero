namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface IHasTenantObject
{
    long? TenantId { get; set; }
}