using Silky.EntityFrameworkCore.Extras.Entities;

namespace Silky.Organization.Domain;

public class OrganizationRole : AuditedEntity
{
    public long OrganizationId { get; set; }

    public long RoleId { get; set; }
    
    public Organization Organization { get; set; }
    
}