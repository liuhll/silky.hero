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
}