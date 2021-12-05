using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.ClaimType.Dtos;
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
    Task CreateOrUpdateAsync(CreateOrUpdateClaimTypeInput input);

    /// <summary>
    /// 通过Id获取声明类型
    /// </summary>
    /// <param name="id">声明类型Id</param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    Task<GetClaimTypeOutput> GetAsync(long id);
    
    /// <summary>
    /// 通过Id删除声明类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    Task DeleteAsync(long id);
}