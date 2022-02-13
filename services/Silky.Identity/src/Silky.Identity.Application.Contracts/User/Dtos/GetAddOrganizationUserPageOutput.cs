namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetAddOrganizationUserPageOutput : UserDtoBase
{
    public long Id { get; set; }
    public long? PositionId { get; set; }

    public string PositionName { get; set; }
}