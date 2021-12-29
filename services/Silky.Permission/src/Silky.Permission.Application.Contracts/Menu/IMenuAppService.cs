using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Rpc.Routing;

namespace Silky.Permission.Application.Contracts.Menu;

/// <summary>
/// 菜单服务
/// </summary>
[ServiceRoute]
public interface IMenuAppService
{
    /// <summary>
    /// 新增/更新菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateAsync(CreateOrUpdateMenuInput input);

    /// <summary>
    /// 通过Id获取菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    Task<GetMenuOutput> GetAsync(long id);

    /// <summary>
    /// 根据id删除菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(long id);

    /// <summary>
    /// 分页获取菜单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetMenuPageOutput>> GetPageAsync(GetMenuPageInput input);
}