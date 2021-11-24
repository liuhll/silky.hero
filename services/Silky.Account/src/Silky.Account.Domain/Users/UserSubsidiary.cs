using Silky.EntityFrameworkCore.Entities;

namespace Silky.Account.Domain.Users;

public class UserSubsidiary : EntityBase<long>
{
    public long OrganizationId { get; set; }

    public long PositionId { get; set; }

    public long UserId { get; set; }
    public virtual User User { get; set; }
}