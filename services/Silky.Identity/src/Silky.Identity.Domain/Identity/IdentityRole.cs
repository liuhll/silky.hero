using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Silky.Core;
using Silky.Core.Extensions.Collections.Generic;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityRole : FullAuditedEntity, IHasConcurrencyStamp
{
    public virtual string Name { get; protected internal set; }

    public virtual string NormalizedName { get; protected internal set; }

    public virtual ICollection<IdentityRoleClaim> Claims { get; protected set; }

    public virtual bool IsDefault { get; set; }

    public virtual bool IsStatic { get; set; }

    public virtual bool IsPublic { get; set; }

    public string ConcurrencyStamp { get; set; }

    public IdentityRole()
    {
    }

    public IdentityRole(long id, [NotNull] string name, Guid? tenantId = null)
    {
        Check.NotNull(name, nameof(name));

        Id = id;
        Name = name;
        TenantId = tenantId;
        NormalizedName = name.ToUpperInvariant();

        Claims = new Collection<IdentityRoleClaim>();
    }

    public virtual void AddClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        Claims.Add(new IdentityRoleClaim(Id, claim, TenantId));
    }

    public virtual void AddClaims([NotNull] IEnumerable<Claim> claims)
    {
        Check.NotNull(claims, nameof(claims));

        foreach (var claim in claims)
        {
            AddClaim(claim);
        }
    }

    public virtual IdentityRoleClaim FindClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        return Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void RemoveClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        Claims.RemoveAll(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void ChangeName(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Name = {Name}";
    }
}