using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Extensions.Collections.Generic;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Domain;

public class IdentityRole : FullAuditedEntity, IHasConcurrencyStamp
{
    public virtual string Name { get; protected internal set; }

    public string RealName { get; protected internal set; }

    public virtual string NormalizedName { get; protected internal set; }

    public virtual ICollection<IdentityRoleClaim> Claims { get; protected set; }

    public virtual ICollection<IdentityRoleOrganization> CustomOrganizationDataRanges { get; protected set; }

    public virtual bool IsDefault { get; set; }

    public virtual bool IsStatic { get; set; }

    public virtual bool IsPublic { get; set; }

    public virtual DataRange DataRange { get; protected internal set; }

    public string ConcurrencyStamp { get; set; }

    public int Sort { get; set; }

    public IdentityRole()
    {
        DataRange = DataRange.SelfOrganization;
    }

    public IdentityRole([NotNull] string name, [NotNull] string realName, object tenantId = null)
    {
        Check.NotNull(name, nameof(name));
        Name = name;
        RealName = realName;
        NormalizedName = name.ToUpperInvariant();

        if (tenantId != null)
        {
            TenantId = long.Parse(tenantId.ToString());
        }

        Claims = new Collection<IdentityRoleClaim>();
        CustomOrganizationDataRanges = new List<IdentityRoleOrganization>();
        DataRange = DataRange.SelfOrganization;
    }

    public virtual void UpdateDataRange(DataRange dataRange)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            CustomOrganizationDataRanges.Clear();
        }

        DataRange = dataRange;
    }

    public virtual void AddOrganizationDataRange(long organizationId)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            DataRange = DataRange.CustomOrganization;
        }
        CustomOrganizationDataRanges.Add(new IdentityRoleOrganization(Id, organizationId));
    }

    public virtual void AddOrganizationDataRanges(IEnumerable<long> organizationIds)
    {
        if (DataRange != DataRange.CustomOrganization)
        {
            DataRange = DataRange.CustomOrganization;
        }

        foreach (var organizationId in organizationIds)
        {
            CustomOrganizationDataRanges.Add(new IdentityRoleOrganization(Id, organizationId));
        }
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

    public void ChangeRealName(string realName)
    {
        Check.NotNullOrWhiteSpace(realName, nameof(realName));

        RealName = realName;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Name = {Name}";
    }
}