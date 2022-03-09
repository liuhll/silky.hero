using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Hero.Common.Enums;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Position.Domain.Shared;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;

namespace Silky.Position.Application.Contracts.Position;

/// <summary>
/// 职位信息服务 
/// </summary>
[ServiceRoute]
[Authorize]
public interface IPositionAppService
{
    /// <summary>
    /// 更新职位信息
    /// </summary>
    /// <remarks>id为空时为新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(PositionPermissions.Positions.Create)]
    [RemoveCachingIntercept(typeof(ICollection<GetPositionOutput>), "allocationOrganizationPositionList")]
    [RemoveCachingIntercept(typeof(ICollection<GetPositionOutput>), "getPublicPositionList")]
    Task CreateAsync(CreatePositionInput input);

    /// <summary>
    /// 更新职位信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveCachingIntercept(typeof(GetPositionOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(bool), "HasPosition:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetPositionOutput>), "allocationOrganizationPositionList")]
    [RemoveCachingIntercept(typeof(ICollection<GetPositionOutput>), "getPublicPositionList")]
    [Authorize(PositionPermissions.Positions.Update)]
    Task UpdateAsync(UpdatePositionInput input);

    /// <summary>
    /// 通过id获取职位信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{0}")]
    [Authorize(PositionPermissions.Positions.LookDetail)]
    Task<GetPositionOutput> GetAsync([CacheKey(0)] long id);

    /// <summary>
    /// 通过Id删除职位信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetPositionOutput), "id:{0}")]
    [RemoveCachingIntercept(typeof(bool), "HasPosition:{0}")]
    [RemoveCachingIntercept(typeof(ICollection<GetPositionOutput>), "allocationOrganizationPositionList")]
    [RemoveCachingIntercept(typeof(ICollection<GetPositionOutput>), "getPublicPositionList")]
    [Authorize(PositionPermissions.Positions.Delete)]
    Task DeleteAsync([CacheKey(0)] long id);

    /// <summary>
    /// 分页查询职位信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<GetPositionPageOutput>> GetPageAsync(GetPositionPageInput input);

    /// <summary>
    /// 通过组织id获取可用于分配的岗位列表
    /// </summary>
    /// <param name="organizationId"></param>
    /// <param name="isAll"></param>
    /// <returns></returns>
    [HttpGet("{organizationId:long}/list")]
    Task<ICollection<GetAllocationOrganizationPositionOutput>> GetPositionListAsync(long organizationId,bool? isAll);

    /// <summary>
    /// 判断是否存在某个职位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("check")]
    Task<bool> CheckAsync(CheckPositionInput input);
    
    /// <summary>
    /// 检查是否有为某个组织机构分配指定职位的数据权限
    /// </summary>
    /// <param name="organizationId"></param>
    /// <param name="positionId"></param>
    /// <returns></returns>
    [HttpPost("check/datarange/{organizationId:long}/{positionId:long}")]
    Task<bool> CheckHasDataRangeAsync(long organizationId, long positionId);

    /// <summary>
    /// 判断是否存在职位
    /// </summary>
    /// <param name="positionId">职位Id</param>
    /// <returns></returns>
    [ProhibitExtranet]
    [GetCachingIntercept("HasPosition:{0}")]
    Task<bool> HasPositionAsync([CacheKey(0)] long positionId);

    /// <summary>
    /// 获取职位列表接口
    /// </summary>
    /// <param name="name"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpGet("list")]
    Task<ICollection<GetPositionOutput>> GetListAsync([FromQuery] string name, [FromQuery] Status? status);

    [GetCachingIntercept("allocationOrganizationPositionList")]
    [ProhibitExtranet]
    Task<ICollection<GetPositionOutput>> GetAllocationOrganizationPositionListAsync();

    [GetCachingIntercept("getPublicPositionList")]
    [ProhibitExtranet]
    Task<ICollection<GetPositionOutput>> GetPublicPositionListAsync();
}