using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Silky.Core;
using Silky.Core.Extensions.Collections.Generic;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;

namespace Silky.Organization.Domain;

public class Organization : FullAuditedEntity, IHasConcurrencyStamp
{
    public Organization()
    {
        ConcurrencyStamp = Guid.NewGuid().ToString();
        Children = new List<Organization>();
        Roles = new List<OrganizationRole>();
    }

    public long? ParentId { get; set; }

    [NotNull] public string Name { get; set; }

    [NotNull] public string Code { get; set; }

    public int Sort { get; set; }

    public string Remark { get; set; }

    public Status Status { get; set; }

    public string ConcurrencyStamp { get; set; }

    public virtual Organization Parent { get; set; }

    public virtual ICollection<Organization> Children { get; set; }

    public virtual ICollection<OrganizationRole> Roles { get; protected set; }

    public virtual void AddRole(long roleId)
    {
        Check.NotNull(roleId, nameof(roleId));

        if (IsInRole(roleId))
        {
            return;
        }

        Roles.Add(new OrganizationRole(roleId, Id, TenantId));
    }

    public virtual void RemoveRole(long roleId)
    {
        Check.NotNull(roleId, nameof(roleId));

        if (!IsInRole(roleId))
        {
            return;
        }

        Roles.RemoveAll(r => r.RoleId == roleId);
    }

    public virtual bool IsInRole(long roleId)
    {
        Check.NotNull(roleId, nameof(roleId));

        return Roles.Any(r => r.RoleId == roleId);
    }
}