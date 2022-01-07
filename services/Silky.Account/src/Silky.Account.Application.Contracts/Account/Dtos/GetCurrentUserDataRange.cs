using System.Collections.Generic;

namespace Silky.Account.Application.Contracts.Account.Dtos;

public class GetCurrentUserDataRange
{
    public long UserId { get; set; }

    public bool IsAllData { get; set; }

    public ICollection<long> OrganizationIds { get; set; }
}