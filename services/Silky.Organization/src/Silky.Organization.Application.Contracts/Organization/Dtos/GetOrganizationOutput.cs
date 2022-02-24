using System;
using System.Collections.Generic;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationOutput : OrganizationDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    public ICollection<GetOrganizationRoleOutput> OrganizationRoles { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
}