using System;

namespace Silky.Organization.Application.Contracts.Organization.Dtos;

public class GetOrganizationOutput : OrganizationDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
}