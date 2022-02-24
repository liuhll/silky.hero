using Silky.Hero.Common.Enums;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationRoleOutput
{
    public long RoleId { get; set; }
    
    public string Name { get; set; }
    
    public string RealName { get; set; }
    public Status Status { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPublic { get; set; }
    public bool IsStatic { get; set; }
}