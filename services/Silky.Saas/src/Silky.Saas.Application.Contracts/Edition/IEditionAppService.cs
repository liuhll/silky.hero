using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Rpc.Routing;
using Silky.Rpc.Security;
using Silky.Saas.Application.Contracts.Edition.Dtos;
using Silky.Saas.Domain.Shared;

namespace Silky.Saas.Application.Contracts.Edition;

[ServiceRoute]
[Authorize]
public interface IEditionAppService
{
    /// <summary>
    /// 新增版本
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(SaasPermissions.Editions.Create)]
    Task CreateAsync(CreateEditionInput input);
    
    /// <summary>
    /// 更新版本
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(SaasPermissions.Editions.Update)]
    Task UpdateAsync(UpdateEditionInput input);
    
    /// <summary>
    /// 删除版本信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(SaasPermissions.Editions.Delete)]
    [HttpDelete("{id:long}")]
    Task DeleteAsync(long id);

    /// <summary>
    /// 分页查询版本信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetEditionPageOutput>> GetPageAsync(GetEditionPageInput input);

    /// <summary>
    /// 设置版本功能
    /// </summary>
    /// <param name="id">版本id</param>
    /// <param name="inputs">输入</param>
    /// <returns></returns>
    [HttpPut("{id:long}/features")]
    [Authorize(SaasPermissions.Editions.SetFeatures)]
    Task SetFeaturesAsync(long id, ICollection<EditionFeatureDto> inputs);

    /// <summary>
    /// 通过Id获取版本信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    Task<GetEditionEditOutput> GetAsync(long id);
}