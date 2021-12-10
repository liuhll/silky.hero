using System;
using Silky.Rpc.CachingInterceptor;

namespace Silky.Tenant.Application.Contracts.Tenant.Dtos;

public class CreateOrUpdateTenantInput : TenantDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [CacheKey(0)]
    public Guid? Id { get; set; }
}