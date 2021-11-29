using System;
using System.Collections.Generic;
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

    public long OrganizationId { get; set; }

    public long PositionId { get; set; }

    public string JobNumber { get; set; }

    public bool LockoutEnabled { get; set; }

    public ICollection<UserSubsidiaryDto> UserSubsidiaries { get; set; }
}