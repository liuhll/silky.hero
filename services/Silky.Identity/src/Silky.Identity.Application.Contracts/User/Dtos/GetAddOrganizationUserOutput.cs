namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetAddOrganizationUserOutput : GetUserPageOutput
{
    public long? PositionId { get; set; }

    public string PositionName { get; set; }
}