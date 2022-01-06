namespace Silky.Hero.Common.Dtos;

public class GetCurrentUserDataRange
{
    public long UserId { get; set; }

    public bool IsAllData { get; set; }

    public ICollection<long> OrganizationIds { get; set; }
}