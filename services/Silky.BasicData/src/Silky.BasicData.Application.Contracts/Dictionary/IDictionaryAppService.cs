using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.BasicData.Application.Contracts.Dictionary.Dtos;
using Silky.Rpc.Routing;

namespace Silky.BasicData.Application.Contracts.Dictionary;

/// <summary>
/// 字典服务
/// </summary>
[ServiceRoute]
public interface IDictionaryAppService
{
    /// <summary>
    /// 新增或更新字典类型服务
    /// </summary>
    /// <remarks>主键Id为空时代表新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    Task CreateOrUpdateTypeAsync(CreateDictionaryTypeInput input);

    /// <summary>
    /// 通过Id获取字典类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("type/{id:long}")]
    Task<GetDictionaryTypeOutput> GetTypeAsync(long id);

    /// <summary>
    /// 通过Id删除字典类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("type/{id:long}")]
    Task DeleteTypeAsync(long id);
    
    /// <summary>
    /// 新增/更新字典项
    /// </summary>
    /// <remarks>主键Id为空时代表新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [HttpPut]
    Task  CreateOrUpdateItemAsync(CreateDictionaryItemInput input);
}