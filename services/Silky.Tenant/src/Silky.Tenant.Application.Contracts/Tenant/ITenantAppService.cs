using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Rpc.Routing;
using Silky.Tenant.Application.Contracts.Tenant.Dtos;

namespace Silky.Tenant.Application.Contracts.Tenant;

[ServiceRoute]
public interface ITenantAppService
{
    /// <summary>
    /// 新增或更新租户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [HttpPost]
    Task CreateOrUpdateAsync(CreateOrUpdateTenantInput input);

    [HttpGet("{id:guid}")]
    Task<GetTenantOutput> GetAsync(Guid id);
}