using JetBrains.Annotations;
using Silky.Hero.Common.Entities;
using Silky.Hero.Common.Enums;

namespace Silky.Position.Domain.Positions;

public class Position : FullAuditedEntity
{
    [NotNull]
    public string Name { get; set; }

    [NotNull]
    public string Code { get; set; }

    public int Sort { get; set; }

    public string Remark { get; set; }

    public Status Status { get; set; }
}