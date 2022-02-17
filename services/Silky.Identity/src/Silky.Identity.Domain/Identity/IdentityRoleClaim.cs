using System.Security.Claims;
using JetBrains.Annotations;

namespace Silky.Identity.Domain;

public class IdentityRoleClaim : IdentityClaim
{
    public virtual long RoleId { get; protected set; }

    protected IdentityRoleClaim()
    {
    }

    protected internal IdentityRoleClaim(
        long roleId,
        [NotNull] Claim claim,
        long? tenantId)
        : base(
            claim,
            tenantId)
    {
        RoleId = roleId;
    }

    public IdentityRoleClaim(
        long roleId,
        [NotNull] string claimType,
        string claimValue,
        long? tenantId)
        : base(
            claimType,
            claimValue,
            tenantId)
    {
        RoleId = roleId;
    }
}