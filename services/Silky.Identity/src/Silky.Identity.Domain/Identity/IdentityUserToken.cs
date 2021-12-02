using System;
using JetBrains.Annotations;
using Silky.Core;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Identity.Domain;

public class IdentityUserToken : AuditedEntity
{
    
    public virtual long UserId { get; protected set; }
    
    public virtual string LoginProvider { get; protected set; }
    
    public virtual string Name { get; protected set; }
    
    public virtual string Value { get; set; }

    protected IdentityUserToken()
    {
    }

    protected internal IdentityUserToken(
        long userId,
        [NotNull] string loginProvider,
        [NotNull] string name,
        string value,
        Guid? tenantId)
    {
        Check.NotNull(loginProvider, nameof(loginProvider));
        Check.NotNull(name, nameof(name));

        UserId = userId;
        LoginProvider = loginProvider;
        Name = name;
        Value = value;
        TenantId = tenantId;
    }
}