
namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetOrganizationUserPageOutput : GetUserPageOutput
{
    public long PositionId { get; set; }

    public string PositionName { get; set; }
}
