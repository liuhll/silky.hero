using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Silky.Core;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityUserLogin : AuditedEntity
{
    public virtual long UserId { get; protected set; }
    public virtual string LoginProvider { get; protected set; }

    public virtual string ProviderKey { get; protected set; }

    public virtual string ProviderDisplayName { get; protected set; }

    protected IdentityUserLogin()
    {
    }

    protected internal IdentityUserLogin(
        long userId,
        [NotNull] string loginProvider,
        [NotNull] string providerKey,
        string providerDisplayName,
        long? tenantId)
    {
        Check.NotNull(loginProvider, nameof(loginProvider));
        Check.NotNull(providerKey, nameof(providerKey));

        UserId = userId;
        LoginProvider = loginProvider;
        ProviderKey = providerKey;
        ProviderDisplayName = providerDisplayName;
        TenantId = tenantId;
    }

    protected internal IdentityUserLogin(
        long userId,
        [NotNull] UserLoginInfo login,
        long? tenantId)
        : this(
            userId,
            login.LoginProvider,
            login.ProviderKey,
            login.ProviderDisplayName,
            tenantId)
    {
    }

    public virtual UserLoginInfo ToUserLoginInfo()
    {
        return new UserLoginInfo(LoginProvider, ProviderKey, ProviderDisplayName);
    }
}