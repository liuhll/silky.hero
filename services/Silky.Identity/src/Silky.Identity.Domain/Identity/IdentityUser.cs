using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Silky.Core;
using Silky.Core.Extensions.Collections.Generic;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Domain;

public class IdentityUser : FullAuditedEntity, IHasConcurrencyStamp
{
    [NotNull] public string UserName { get; set; }

    [NotNull] public string NormalizedUserName { get; protected internal set; }

    [NotNull] public string RealName { get; set; }

    [NotNull] public string PasswordHash { get; set; }

    public string Surname { get; set; }

    public DateTime? BirthDay { get; set; }

    public Sex? Sex { get; set; }

    [NotNull] public string Email { get; protected internal set; }

    [NotNull] public string NormalizedEmail { get; protected internal set; }

    public string TelPhone { get; set; }

    [NotNull] public string MobilePhone { get; protected internal set; }
    
    public string JobNumber { get; set; }

    public Status Status { get; set; }

    public string SecurityStamp { get; protected internal set; }
    public bool LockoutEnabled { get; protected internal set; }

    public int AccessFailedCount { get; protected internal set; }

    public string ConcurrencyStamp { get; set; }

    public bool EmailConfirmed { get; protected internal set; }

    public bool PhoneNumberConfirmed { get; protected internal set; }

    public DateTimeOffset? LockoutEnd { get; protected internal set; }

    public virtual ICollection<UserSubsidiary> UserSubsidiaries { get; protected set; }

    public virtual ICollection<IdentityUserRole> Roles { get; protected set; }

    public virtual ICollection<IdentityUserClaim> Claims { get; protected set; }

    public virtual ICollection<IdentityUserLogin> Logins { get; protected set; }

    public virtual ICollection<IdentityUserToken> Tokens { get; protected set; }


    public IdentityUser()
    {
        Roles = new Collection<IdentityUserRole>();
        Claims = new Collection<IdentityUserClaim>();
        Logins = new Collection<IdentityUserLogin>();
        Tokens = new Collection<IdentityUserToken>();
        UserSubsidiaries = new List<UserSubsidiary>();
    }

    public IdentityUser(
        [NotNull] string userName,
        [NotNull] string email,
        [NotNull] string mobilePhone,
        object tenantId = null) : this()
    {
        Check.NotNull(userName, nameof(userName));
        Check.NotNull(email, nameof(email));
        Check.NotNull(mobilePhone, nameof(mobilePhone));

        if (tenantId != null)
        {
            TenantId = long.Parse(tenantId.ToString());
        }

        UserName = userName;
        NormalizedUserName = userName.ToUpperInvariant();
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
        MobilePhone = mobilePhone;
        ConcurrencyStamp = Guid.NewGuid().ToString();
        SecurityStamp = Guid.NewGuid().ToString();
        Status = Status.Valid;
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

    public void SetPhoneNumberConfirmed(bool confirmed)
    {
        PhoneNumberConfirmed = confirmed;
    }

    public void SetEmailConfirmed(bool confirmed)
    {
        EmailConfirmed = confirmed;
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

    public virtual void AddUserSubsidiaries(long organizationId, long positionId, bool isLeader)
    {
        if (IsInUserSubsidiaries(organizationId, positionId))
        {
            return;
        }
        
        UserSubsidiaries.Add(
            new UserSubsidiary(
                Id,
                organizationId,
                positionId,
                isLeader,
                TenantId
            )
        );
    }

    public bool IsInUserOrganization(long organizationId)
    {
        return UserSubsidiaries.Any(
            us => us.OrganizationId == organizationId
        );
    }

    public virtual void RemoveUserSubsidiaries(long organizationId, long positionId)
    {
        if (!IsInUserSubsidiaries(organizationId, positionId))
        {
            return;
        }

        UserSubsidiaries.RemoveAll(
            us => us.OrganizationId == organizationId && us.PositionId == positionId
        );
    }

    public virtual bool IsInUserSubsidiaries(long organizationId, long positionId)
    {
        return UserSubsidiaries.Any(
            us => us.OrganizationId == organizationId
                  && us.PositionId == positionId
        );
    }

    
    public override string ToString()
    {
        return $"{base.ToString()}, UserName = {UserName}";
    }
}