using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

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
    [RemoveCachingIntercept(typeof(GetPositionOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(bool), "HasPosition:{0}")]
    Task CreateOrUpdateAsync(CreateOrUpdatePositionInput input);

    /// <summary>
    /// 通过id获取职位信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetPositionOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 通过Id删除职位信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetPositionOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(bool), "HasPosition:{0}")]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 分页查询职位信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetPositionPageOutput>> GetPageAsync(GetPositionPageInput input);
    
    /// <summary>
    /// 判断是否存在职位
    /// </summary>
    /// <param name="positionId">职位Id</param>
    /// <returns></returns>
    [Governance(ProhibitExtranet = true)]
    [GetCachingIntercept("HasPosition:{0}")]
    Task<bool> HasPositionAsync([CacheKey(0)]long positionId);
}