using System;
using System.Diagnostics.CodeAnalysis;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class CreateOrUpdateUserInput
{
    public long? Id { get; set; }

    [NotNull] public string UserName { get; set; }

    [NotNull] public string RealName { get; set; }

    [NotNull] public string Password { get; set; }

    public string Surname { get; set; }

    public DateTime BirthDay { get; set; }

    public Sex? Sex { get; set; }

    [NotNull] public string Email { get; set; }

    public string TelPhone { get; set; }

    [NotNull] public string MobilePhone { get; set; }

    public long OrganizationId { get; set; }

    public long PositionId { get; set; }

    public string JobNumber { get; set; }

}