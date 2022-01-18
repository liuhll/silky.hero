using System;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetUserPageOutput
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

    public DateTimeOffset CreatedTime { get; set; }
}