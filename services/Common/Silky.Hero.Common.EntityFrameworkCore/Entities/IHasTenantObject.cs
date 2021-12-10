namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface IHasTenantObject
{
    Guid? TenantId { get; set; }
}