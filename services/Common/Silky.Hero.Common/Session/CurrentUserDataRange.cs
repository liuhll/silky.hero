namespace Silky.Hero.Common.Session;

public class CurrentUserDataRange
{
    public long UserId { get; set; }

    public bool IsAllData { get; set; }

    public ICollection<long> OrganizationIds { get; set; }
}