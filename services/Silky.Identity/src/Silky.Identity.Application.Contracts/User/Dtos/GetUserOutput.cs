using System;
using System.Collections.Generic;
using Silky.Hero.Common.Enums;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetUserOutput 
{
    public long Id { get; set; }

    public string UserName { get; set; }

    public string RealName { get; set; }

    public string Surname { get; set; }

    public DateTime BirthDay { get; set; }

    public Sex? Sex { get; set; }

    public string Email { get; set; }
    
    public string TelPhone { get; set; }

    public string MobilePhone { get; set; }

    public string JobNumber { get; set; }
    
    public Status Status { get; set; }
    
    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool IsLockout {
        get
        {
            if (!LockoutEnd.HasValue)
            {
                return false;
            }

            return DateTimeOffset.Now <= LockoutEnd.Value;
        }
    }

    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }

    public ICollection<GetUserSubsidiaryOutput> UserSubsidiaries { get; set; }

    public ICollection<GetUserRolePageOutput> Roles { get; set; }
}