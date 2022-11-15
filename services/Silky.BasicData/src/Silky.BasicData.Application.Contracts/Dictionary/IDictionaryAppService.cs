using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.BasicData.Application.Contracts.Dictionary.Dtos;
using Silky.BasicData.Domain.Shared;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.BasicData.Application.Contracts.Dictionary;

/// <summary>
/// 字典服务
/// </summary>
[ServiceRoute]
[Authorize]
public interface IDictionaryAppService
{
    /// <summary>
    /// 新增字典类型服务
    /// </summary>
    /// <remarks>主键Id为空时代表新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(BasicDataPermissions.Dictionaries.Types.Create)]
    Task CreateTypeAsync(CreateDictionaryTypeInput input);
    
    /// <summary>
    /// 更新字典类型服务
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetDictionaryTypeOutput),"type:id:{Id}")]
    [Authorize(BasicDataPermissions.Dictionaries.Types.Update)]
    Task UpdateTypeAsync(UpdateDictionaryTypeInput input);

    /// <summary>
    /// 通过Id获取字典类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("type/{id:long}")]
    [GetCachingIntercept("type:id:{id}")]
    [Authorize(BasicDataPermissions.Dictionaries.Types.LookDetail)]
    Task<GetDictionaryTypeOutput> GetTypeAsync(long id);

    /// <summary>
    /// 通过Id删除字典类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("type/{id:long}")]
    [RemoveCachingIntercept(typeof(GetDictionaryTypeOutput),"type:id:{id}")]
    [Authorize(BasicDataPermissions.Dictionaries.Types.Delete)]
    Task DeleteTypeAsync(long id);

    /// <summary>
    /// 分页获取字典类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("type/page")]
    Task<PagedList<GetDictionaryTypePageOutput>> GetTypePageAsync(GetDictionaryTypePageInput input);

    /// <summary>
    /// 新增字典项
    /// </summary>
    /// <remarks>主键Id为空时代表新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(BasicDataPermissions.Dictionaries.Items.Create)]
    Task CreateItemAsync(CreateDictionaryItemInput input);
    
    /// <summary>
    /// 更新字典项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetDictionaryItemOutput),"item:id:{id}")]
    [Authorize(BasicDataPermissions.Dictionaries.Items.Update)]
    Task UpdateItemAsync(UpdateDictionaryItemInput input);

    /// <summary>
    /// 通过Id获取字典项
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("item/{id:long}")]
    [GetCachingIntercept("item:id:{id}")]
    [Authorize(BasicDataPermissions.Dictionaries.Items.LookDetail)]
    Task<GetDictionaryItemOutput> GetItemAsync(long id);

    /// <summary>
    /// 根据Id删除字典项
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("item/{id:long}")]
    [RemoveCachingIntercept(typeof(GetDictionaryItemOutput),"item:id:{id}")]
    [Authorize(BasicDataPermissions.Dictionaries.Items.Delete)]
    Task DeleteItemAsync(long id);

    /// <summary>
    /// 分页获取字典项接口
    /// </summary>
    /// <param name="dictionaryId">字典id</param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("{dictionaryId:long}/items/page")]
    Task<PagedList<GetDictionaryItemPageOutput>> GetItemPageAsync(long dictionaryId, GetDictionaryItemPageInput input);

    /// <summary>
    /// 根据Id获取所有字典项项目
    /// </summary>
    /// <param name="dictionaryId"></param>
    /// <returns></returns>
    [HttpGet("{dictionaryId:long}/items")]
    [GetCachingIntercept("items:dictionaryId:{dictionaryId}")]
    Task<ICollection<GetDictionaryItemOutput>> GetAllItemsByIdAsync(long dictionaryId);
    
    /// <summary>
    /// 根据Code获取所有字典项
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("{code}/items")]
    [GetCachingIntercept("items:code:{code}")]
    Task<ICollection<GetDictionaryItemOutput>> GetAllItemsByCodeAsync(string code);
}