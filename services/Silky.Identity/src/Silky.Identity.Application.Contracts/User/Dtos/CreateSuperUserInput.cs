namespace Silky.Identity.Application.Contracts.User.Dtos;

public class CreateSuperUserInput : UserDtoBase
{
    public long TenantId { get; set; }
    public string RoleName { get; set; }
}