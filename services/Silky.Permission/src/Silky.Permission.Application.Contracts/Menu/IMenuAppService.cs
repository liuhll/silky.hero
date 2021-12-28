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
}