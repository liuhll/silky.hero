namespace Silky.Hero.Common.EntityFrameworkCore.Entities;

public interface IAuditedObject : ICreatedObject, IUpdatedObject, IHasTenantObject
{
}