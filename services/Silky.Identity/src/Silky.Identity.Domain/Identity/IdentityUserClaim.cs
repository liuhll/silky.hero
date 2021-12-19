using System;
using System.Security.Claims;
using JetBrains.Annotations;

namespace Silky.Identity.Domain;

public class IdentityUserClaim : IdentityClaim
{
    public virtual long UserId { get; protected set; }

    public IdentityUserClaim()
    {
    }

    public IdentityUserClaim(
        long userId,
        [NotNull] Claim claim,
        long? tenantId = null)
        : base(
            claim,
            tenantId)
    {
        UserId = userId;
    }

    public IdentityUserClaim(
        long userId,
        [NotNull] string claimType,
        string claimValue,
        long? tenantId = null)
        : base(
            claimType,
            claimValue,
            tenantId)
    {
        UserId = userId;
    }
}