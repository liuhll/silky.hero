using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Rpc.Routing;

namespace Silky.Position.Application.Contracts.Position;

/// <summary>
/// 职位信息服务 
/// </summary>
[ServiceRoute]
public interface IPositionAppService
{
    /// <summary>
    /// 新增/更新职位信息
    /// </summary>
    /// <remarks>id为空时为新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateAsync(CreateOrUpdatePositionInput input);

    /// <summary>
    /// 通过id获取职位信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    Task<GetPositionOutput> GetAsync(long id);
    
    /// <summary>
    /// 通过Id删除职位信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    Task DeleteAsync(long id);

    /// <summary>
    /// 分页查询职位信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetPositionPageOutput>> GetPageAsync(GetPositionPageInput input);
}