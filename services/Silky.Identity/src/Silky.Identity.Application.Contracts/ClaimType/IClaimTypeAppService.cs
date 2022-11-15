using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.ClaimType.Dtos;
using Silky.Identity.Domain.Shared;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.Identity.Application.Contracts.ClaimType;

/// <summary>
/// 声明类型服务
/// </summary>
[ServiceRoute]
[Authorize]
public interface IClaimTypeAppService
{
    /// <summary>
    /// 新增声明类型
    /// </summary>
    /// <param name="input">声明dto对象</param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(ICollection<GetClaimTypeOutput>), "all")]
    [Authorize(IdentityPermissions.ClaimTypes.Create)]
    Task CreateAsync(CreateClaimTypeInput input);

    /// <summary>
    /// 更新声明类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetClaimTypeOutput), "id:{id}")]
    [RemoveCachingIntercept(typeof(ICollection<GetClaimTypeOutput>), "all")]
    [Authorize(IdentityPermissions.ClaimTypes.Update)]
    Task UpdateAsync(UpdateClaimTypeInput input);

    /// <summary>
    /// 通过Id获取声明类型
    /// </summary>
    /// <param name="id">声明类型Id</param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{id}")]
    Task<GetClaimTypeOutput> GetAsync(long id);

    /// <summary>
    /// 通过Id删除声明类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetClaimTypeOutput), "id:{id}")]
    [RemoveCachingIntercept(typeof(ICollection<GetClaimTypeOutput>), "all")]
    [Authorize(IdentityPermissions.ClaimTypes.Delete)]
    Task DeleteAsync(long id);

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