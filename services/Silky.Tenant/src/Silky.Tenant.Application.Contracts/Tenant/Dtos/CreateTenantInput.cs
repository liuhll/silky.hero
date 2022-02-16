namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class CreateTenantInput : TenantDtoBase
{
    public string SuperUserName { get; set; }

    public string SuperUserEmail { get; set; }
    
    public string SuperPassword { get; set; }

    public string SuperRoleName { get; set; }
    
    public string SuperRoleRealName { get; set; }
    public string SuperUserMobilePhone { get; set; }
    public string SuperUserJobNumber { get; set; }
    public string SuperRealName { get; set; }
}