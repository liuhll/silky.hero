using System.Collections.Generic;
using Silky.Hero.Common.Enums;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationTreeOutput
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public string Remark { get; set; }

    public Status Status { get; set; }

    public virtual ICollection<GetOrganizationTreeOutput> Children { get; set; }
}