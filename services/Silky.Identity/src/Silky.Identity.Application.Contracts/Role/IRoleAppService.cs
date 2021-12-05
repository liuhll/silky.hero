using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Rpc.Routing;

namespace Silky.Identity.Application.Contracts.Role;

/// <summary>
/// 角色信息服务
/// </summary>
[ServiceRoute]
public interface IRoleAppService
{
    /// <summary>
    /// 新增/更新角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateAsync(CreateOrUpdateRoleInput input);

    /// <summary>
    /// 根据id获取角色信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    Task<GetRoleOutput> GetAsync(long id);
}