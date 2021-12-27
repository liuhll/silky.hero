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
    /// 新增/更新菜单目录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateCatalogAsync(CreateOrUpdateCatalogInput input);
    
    /// <summary>
    /// 创建/更新菜单
    /// </summary>
    /// <param name="input">输入参数</param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateMenuAsync(CreateOrUpdateMenuInput input);
    
    
}