using System;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class GetTenantOutput : TenantDtoBase
{
    public Guid Id { get; set; }
}