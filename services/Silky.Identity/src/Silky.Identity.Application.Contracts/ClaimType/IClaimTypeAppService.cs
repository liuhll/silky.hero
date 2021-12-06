using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.ClaimType.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;

namespace Silky.Identity.Application.Contracts.ClaimType;

/// <summary>
/// 声明类型服务
/// </summary>
[ServiceRoute]
public interface IClaimTypeAppService
{
    /// <summary>
    /// 新增/更新声明类型
    /// </summary>
    /// <param name="input">声明dto对象</param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    [RemoveCachingIntercept(typeof(GetClaimTypeOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetClaimTypeOutput>),"all")]
    Task CreateOrUpdateAsync(CreateOrUpdateClaimTypeInput input);

    /// <summary>
    /// 通过Id获取声明类型
    /// </summary>
    /// <param name="id">声明类型Id</param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    Task<GetClaimTypeOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 通过Id删除声明类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetClaimTypeOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetClaimTypeOutput>),"all")]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 分页查询声明类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetClaimTypePageOutput>> GetPageAsync(GetClaimTypePageInput input);

    /// <summary>
    /// 获取所有的声明类型
    /// </summary>
    /// <returns>返回所有的声明类型</returns>
    [GetCachingIntercept("all")]
    Task<ICollection<GetClaimTypeOutput>> GetAllAsync();
}