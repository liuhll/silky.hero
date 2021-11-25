using Silky.EntityFrameworkCore.Entities;

namespace Silky.Hero.Common.Entities;

public abstract class AuditedEntity: Entity<long>
{
    protected AuditedEntity()
    {
    }
    
}