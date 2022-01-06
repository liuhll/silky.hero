using System.Collections.Generic;

namespace Silky.Identity.Domain;

public class UserDataRange
{
    internal UserDataRange(long userId, bool isAllData, ICollection<long> organizationIds = null)
    {
        UserId = userId;
        IsAllData = isAllData;
        OrganizationIds = organizationIds;
    }

    public long UserId { get; private set; }

    public bool IsAllData { get; private set; }

    public ICollection<long> OrganizationIds { get; private set; }
}