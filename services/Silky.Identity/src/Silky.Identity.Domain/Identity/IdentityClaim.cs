using System;
using System.Security.Claims;
using JetBrains.Annotations;
using Silky.Core;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public abstract class IdentityClaim : AuditedEntity
{
    public virtual string ClaimType { get; protected set; }

    public virtual string ClaimValue { get; protected set; }

    protected IdentityClaim()
    {
    }

    protected internal IdentityClaim([NotNull] Claim claim, Guid? tenantId)
        : this(claim.Type, claim.Value, tenantId)
    {
    }

    protected internal IdentityClaim([NotNull] string claimType, string claimValue, Guid? tenantId)
    {
        Check.NotNull(claimType, nameof(claimType));
        
        ClaimType = claimType;
        ClaimValue = claimValue;
        TenantId = tenantId;
    }

    public virtual Claim ToClaim()
    {
        return new Claim(ClaimType, ClaimValue);
    }

    public virtual void SetClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        ClaimType = claim.Type;
        ClaimValue = claim.Value;
    }
}