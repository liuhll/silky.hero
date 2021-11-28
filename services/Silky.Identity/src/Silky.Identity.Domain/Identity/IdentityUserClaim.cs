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
        Guid? tenantId)
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
        Guid? tenantId)
        : base(
            claimType,
            claimValue,
            tenantId)
    {
        UserId = userId;
    }
}