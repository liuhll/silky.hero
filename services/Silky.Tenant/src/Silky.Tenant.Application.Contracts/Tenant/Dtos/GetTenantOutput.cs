using System;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class GetTenantOutput : TenantDtoBase
{
    public long Id { get; set; }
}