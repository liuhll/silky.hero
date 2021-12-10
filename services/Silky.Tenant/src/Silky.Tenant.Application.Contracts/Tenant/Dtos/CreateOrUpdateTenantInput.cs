using System;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class CreateOrUpdateTenantInput : TenantDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid? Id { get; set; }
}