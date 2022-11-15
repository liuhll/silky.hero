using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Silky.EntityFrameworkCore.Extras.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;

namespace Silky.Organization.Domain;

public class Organization : FullAuditedEntity, IHasConcurrencyStamp
{
    public Organization()
    {
        ConcurrencyStamp = Guid.NewGuid().ToString();
        Children = new List<Organization>();
        OrganizationRoles = new List<OrganizationRole>();
        OrganizationPositions = new List<OrganizationPosition>();
    }

    public long? ParentId { get; set; }

    [NotNull] public string Name { get; set; }

    public int Sort { get; set; }

    public string Remark { get; set; }

    public Status Status { get; set; }

    public string ConcurrencyStamp { get; set; }

    public virtual Organization Parent { get; set; }

    public virtual ICollection<Organization> Children { get; set; }

    public virtual ICollection<OrganizationRole> OrganizationRoles { get; set; }

    public virtual ICollection<OrganizationPosition> OrganizationPositions { get; set; }
    
    public void SetRoles(params long[] roleIds)
    {
        foreach (var roleId in roleIds)
        {
            if (OrganizationRoles.Any(p => p.RoleId == roleId))
            {
                continue;
            }

            OrganizationRoles.Add(new OrganizationRole()
            {
                OrganizationId = Id,
                RoleId = roleId
            });
        }
    }

    public void SetPositions(params long[] positionIds)
    {
        foreach (var positionId in positionIds)
        {
            if (OrganizationPositions.Any(p => p.PositionId == positionId))
            {
                continue;
            }

            OrganizationPositions.Add(new OrganizationPosition()
            {
                OrganizationId = Id,
                PositionId = positionId
            });
        }
    }
}