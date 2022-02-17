using System;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class GetTenantPageOutput : GetTenantOutput
{
    public DateTimeOffset CreatedTime { get; set; }
    
    public DateTimeOffset? UpdatedTime { get; set; }
}