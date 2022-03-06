namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetUserSubsidiaryOutput
{
    public long PositionId { get; set; }

    public string PositionName { get; set; }
    
    public long OrganizationId { get; set; }
    
    public string OrganizationName { get; set; }
    
    public bool IsLeader { get; set; }

}