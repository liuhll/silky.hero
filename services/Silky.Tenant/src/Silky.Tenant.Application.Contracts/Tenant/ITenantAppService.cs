using System;
using System.Collections.Generic;
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

    /// <summary>
    /// 通过Id获取租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    Task<GetTenantOutput> GetAsync(Guid id);

    /// <summary>
    /// 通过id删除租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 分页查询租户信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetTenantPageOutput>> GetPageAsync(GetTenantPageInput input);
}