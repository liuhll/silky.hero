using System;
using Silky.Identity.Domain.Shared;

namespace Silky.Account.Application.Contracts.Account.Dtos;

public class GetCurrentUserInfoOutput
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
}