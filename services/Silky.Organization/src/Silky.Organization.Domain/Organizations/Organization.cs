using System.Diagnostics.CodeAnalysis;
using Silky.Hero.Common.Entities;
using Silky.Hero.Common.Enums;

namespace Silky.Organization.Domain.Organizations;

public class Organization : FullAuditedEntity
{
    public long? ParentId { get; set; }
    
    [NotNull]
    public string Name { get; set; }

    [NotNull]
    public string Code { get; set; }

    public int Sort { get; set; }

    public string Remark { get; set; }

    public Status Status { get; set; }
    
}