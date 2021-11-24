using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Silky.Account.Domain.Shared.Users;
using Silky.Hero.Common.Entities;

namespace Silky.Account.Domain.Users;

public class User : FullAuditedEntity 
{
    [NotNull]
    public string UserName { get; set; }

    [NotNull]
    public string RealName { get; set; }

    [NotNull]
    public string Password { get; set; }

    public string NickName { get; set; }

    public DateTime BirthDay { get; set; }

    public Sex? Sex { get; set; }

    [NotNull]
    public string Email { get; set; }

    public string TelPhone { get; set; }

    [NotNull]
    public string MobilePhone { get; set; }

    public long OrganizationId { get; set; }
    
    public long PositionId { get; set; }

    public string JobNumber { get; set; }

    public virtual ICollection<UserSubsidiary> UserSubsidiaries { get; set; }

}