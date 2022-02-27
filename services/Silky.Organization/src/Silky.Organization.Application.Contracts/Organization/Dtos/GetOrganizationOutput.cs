using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Position.Application.Contracts.Position.Dtos;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationOutput : OrganizationDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    public ICollection<GetOrganizationRoleOutput> OrganizationRoles { get; set; }
    
    public ICollection<GetOrganizationPositionOutput> OrganizationPositions { get; set; }

    public bool IsBelong { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
    
}