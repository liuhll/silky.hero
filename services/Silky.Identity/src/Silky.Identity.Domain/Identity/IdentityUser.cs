using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Silky.Core;
using Silky.Core.Extensions.Collections.Generic;
using Silky.Hero.Common.Entities;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Domain;

public class IdentityUser : FullAuditedEntity, IHasConcurrencyStamp
{
    [NotNull] public string UserName { get; protected internal set; }

    [NotNull] public string NormalizedUserName { get; protected internal set; }

    [NotNull] public string RealName { get; set; }

    [NotNull] public string PasswordHash { get; set; }

    public string Surname { get; set; }

    public DateTime BirthDay { get; set; }

    public Sex? Sex { get; set; }

    [NotNull] public string Email { get; set; }

    [NotNull] public string NormalizedEmail { get; set; }

    public string TelPhone { get; set; }

    [NotNull] public string MobilePhone { get; set; }

    public long OrganizationId { get; set; }

    public long PositionId { get; set; }

    public string JobNumber { get; set; }

    public bool IsActive { get; protected internal set; }

    public string SecurityStamp { get; protected internal set; }
    public bool LockoutEnabled { get; protected internal set; }

    public int AccessFailedCount { get; protected internal set; }

    public string ConcurrencyStamp { get; set; }


    public virtual ICollection<UserSubsidiary> UserSubsidiaries { get; set; }

    public virtual ICollection<IdentityUserRole> Roles { get; protected set; }

    public virtual ICollection<IdentityUserClaim> Claims { get; protected set; }

    public virtual ICollection<IdentityUserLogin> Logins { get; protected set; }

    public virtual ICollection<IdentityUserToken> Tokens { get; protected set; }

    protected IdentityUser()
    {
    }

    public IdentityUser(
        [NotNull] string userName,
        [NotNull] string email,
        Guid? tenantId = null)
    {
        Check.NotNull(userName, nameof(userName));
        Check.NotNull(email, nameof(email));

        TenantId = tenantId;
        UserName = userName;
        NormalizedUserName = userName.ToUpperInvariant();
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
        ConcurrencyStamp = Guid.NewGuid().ToString();
        SecurityStamp = Guid.NewGuid().ToString();
        IsActive = true;

        Roles = new Collection<IdentityUserRole>();
        Claims = new Collection<IdentityUserClaim>();
        Logins = new Collection<IdentityUserLogin>();
        Tokens = new Collection<IdentityUserToken>();
        UserSubsidiaries = new List<UserSubsidiary>();
    }

    public virtual void AddRole(long roleId)
    {
        Check.NotNull(roleId, nameof(roleId));

        if (IsInRole(roleId))
        {
            return;
        }

        Roles.Add(new IdentityUserRole(Id, roleId, TenantId));
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

    public virtual void AddClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        Claims.Add(new IdentityUserClaim(Id, claim, TenantId));
    }

    public virtual void AddClaims([NotNull] IEnumerable<Claim> claims)
    {
        Check.NotNull(claims, nameof(claims));

        foreach (var claim in claims)
        {
            AddClaim(claim);
        }
    }

    public virtual IdentityUserClaim FindClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        return Claims.FirstOrDefault(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
    }

    public virtual void ReplaceClaim([NotNull] Claim claim, [NotNull] Claim newClaim)
    {
        Check.NotNull(claim, nameof(claim));
        Check.NotNull(newClaim, nameof(newClaim));

        var userClaims = Claims.Where(uc => uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type);
        foreach (var userClaim in userClaims)
        {
            userClaim.SetClaim(newClaim);
        }
    }

    public virtual void RemoveClaims([NotNull] IEnumerable<Claim> claims)
    {
        Check.NotNull(claims, nameof(claims));

        foreach (var claim in claims)
        {
            RemoveClaim(claim);
        }
    }

    public virtual void RemoveClaim([NotNull] Claim claim)
    {
        Check.NotNull(claim, nameof(claim));

        Claims.RemoveAll(c => c.ClaimValue == claim.Value && c.ClaimType == claim.Type);
    }

    public virtual void AddLogin([NotNull] UserLoginInfo login)
    {
        Check.NotNull(login, nameof(login));

        Logins.Add(new IdentityUserLogin(Id, login, TenantId));
    }

    public virtual void RemoveLogin([NotNull] string loginProvider, [NotNull] string providerKey)
    {
        Check.NotNull(loginProvider, nameof(loginProvider));
        Check.NotNull(providerKey, nameof(providerKey));

        Logins.RemoveAll(userLogin =>
            userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey);
    }

    [CanBeNull]
    public virtual IdentityUserToken FindToken(string loginProvider, string name)
    {
        return Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name);
    }

    public virtual void SetToken(string loginProvider, string name, string value)
    {
        var token = FindToken(loginProvider, name);
        if (token == null)
        {
            Tokens.Add(new IdentityUserToken(Id, loginProvider, name, value, TenantId));
        }
        else
        {
            token.Value = value;
        }
    }

    public virtual void RemoveToken(string loginProvider, string name)
    {
        Tokens.RemoveAll(t => t.LoginProvider == loginProvider && t.Name == name);
    }

    public virtual void AddSubsidiaries(long organizationId, long positionId)
    {
        if (IsInSubsidiaries(organizationId, positionId))
        {
            return;
        }

        UserSubsidiaries.Add(
            new UserSubsidiary(
                Id,
                organizationId,
                positionId,
                TenantId
            )
        );
    }

    public virtual void RemoveOrganizationUnit(long organizationId, long positionId)
    {
        if (!IsInSubsidiaries(organizationId, positionId))
        {
            return;
        }

        UserSubsidiaries.RemoveAll(
            us => us.OrganizationId == organizationId && us.PositionId == positionId
        );
    }

    public virtual bool IsInSubsidiaries(long organizationId, long positionId)
    {
        return UserSubsidiaries.Any(
            us => us.OrganizationId == organizationId
                  && us.PositionId == positionId
        );
    }

    public virtual void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, UserName = {UserName}";
    }
}