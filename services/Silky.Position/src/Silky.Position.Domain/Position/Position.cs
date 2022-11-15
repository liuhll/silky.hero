using System;
using JetBrains.Annotations;
using Silky.EntityFrameworkCore.Extras.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;

namespace Silky.Position.Domain;

public class Position : FullAuditedEntity, IHasConcurrencyStamp
{
    public Position()
    {
        ConcurrencyStamp = Guid.NewGuid().ToString();
    }

    [NotNull] public string Name { get; set; }
    
    public int Sort { get; set; }

    public string Remark { get; set; }

    public virtual bool IsStatic { get; set; }
    
    public virtual bool IsPublic { get; set; }
    
    public Status Status { get; set; }
    
    public string ConcurrencyStamp { get; set; }
    
}