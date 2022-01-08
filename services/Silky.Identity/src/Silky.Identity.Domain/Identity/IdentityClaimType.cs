using JetBrains.Annotations;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Domain;

public class IdentityClaimType : FullAuditedEntity
{
    public virtual string Name { get; protected set; }

    public virtual bool Required { get; set; }

    public virtual bool IsStatic { get; protected set; }

    public virtual string Regex { get; set; }

    public virtual string RegexDescription { get; set; }

    public virtual string Description { get; set; }

    public virtual IdentityClaimValueType ValueType { get; set; }

    public IdentityClaimType()
    {
    }

    public IdentityClaimType(
        [NotNull] string name,
        bool required = false,
        bool isStatic = false,
        [CanBeNull] string regex = null,
        [CanBeNull] string regexDescription = null,
        [CanBeNull] string description = null,
        IdentityClaimValueType valueType = IdentityClaimValueType.String,
        object tenantId = null)
    {
        SetName(name);
        Required = required;
        IsStatic = isStatic;
        Regex = regex;
        RegexDescription = regexDescription;
        Description = description;
        ValueType = valueType;
        if (tenantId != null)
        {
            TenantId = long.Parse(tenantId.ToString());
        }
    }

    public void SetName([NotNull] string name)
    {
        Name = Check.NotNull(name, nameof(name));
    }
}