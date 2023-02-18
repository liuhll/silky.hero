﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Organization.Application.Contracts.Organization.Dtos;
using Silky.Organization.Domain.Shared;
using Silky.Position.Application.Contracts.Position.Dtos;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;
using Silky.Transaction;

namespace Silky.Organization.Application.Contracts.Organization;

/// <summary>
/// 组织机构服务
/// </summary>
[ServiceRoute]
[Authorize]
public interface IOrganizationAppService
{
    /// <summary>
    /// 新增/更新组织机构
    /// </summary>
    /// <remarks>如果Id为null，则表示新增</remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveMatchKeyCachingIntercept(typeof(ICollection<GetOrganizationTreeOutput>),"*:OrganizationTree:userId:*")]
    [Authorize(OrganizationPermissions.Organizations.Create)]
    Task CreateAsync(CreateOrganizationInput input);

    /// <summary>
    /// 检查是否存在某个组织机构
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("check")]
    Task<bool> CheckAsync(CheckOrganizationInput input);

    /// <summary>
    /// 检查是否数据权限
    /// </summary>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    [HttpPost("check/datarange/{organizationId:long}")]
    Task<bool> CheckHasDataRangeAsync(long organizationId);

    /// <summary>
    /// 更新组织机构
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [RemoveMatchKeyCachingIntercept(typeof(GetOrganizationOutput), "id:{Id}:*")]
    [RemoveMatchKeyCachingIntercept(typeof(ICollection<GetOrganizationTreeOutput>),"OrganizationTree:userId:*")]
    [RemoveCachingIntercept(typeof(bool), "HasOrganization:{Id}")]
    [RemoveCachingIntercept(typeof(ICollection<long>), "GetSelfAndChildrenOrganizationIds:{Id}")]
    [Authorize(OrganizationPermissions.Organizations.Update)]
    Task UpdateAsync(UpdateOrganizationInput input);

    /// <summary>
    /// 删除组织机构
    /// </summary>
    /// <param name="id">主键Id</param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [RemoveCachingIntercept(typeof(GetOrganizationOutput), "id:{id}:*")]
    [RemoveMatchKeyCachingIntercept(typeof(ICollection<GetOrganizationTreeOutput>),"*:OrganizationTree:userId:*")]
    [RemoveCachingIntercept(typeof(bool), "HasOrganization:{id}")]
    [RemoveCachingIntercept(typeof(ICollection<long>), "GetSelfAndChildrenOrganizationIds:{id}")]
    [Authorize(OrganizationPermissions.Organizations.Delete)]
    [Transaction]
    Task DeleteAsync(long id);

    /// <summary>
    /// 通过Id获取组织机构
    /// </summary>
    /// <param name="id">主键Id</param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [GetCachingIntercept("id:{id}", OnlyCurrentUserData = true)]
    Task<GetOrganizationOutput> GetAsync(long id);

    /// <summary>
    /// 获取某个组织机构的用户列表
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("{id:long}/user/page")]
    Task<PagedList<GetOrganizationUserPageOutput>> GetUserPageAsync(long id, GetOrganizationUserPageInput input);

    /// <summary>
    /// 获取指定组织机构的用户Ids
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}/userids")]
    [Authorize(OrganizationPermissions.Organizations.AddUsers)]
    Task<ICollection<long>> GetUserIdsAsync(long id);

    /// <summary>
    /// 添加指定组织机构用户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [HttpPut("{id:long}/users")]
    [Authorize(OrganizationPermissions.Organizations.AddUsers)]
    Task AddUsers(long id, ICollection<AddOrganizationUserInput> inputs);

    /// <summary>
    /// 移除指定组织机构用户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userIds"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}/users")]
    [Authorize(OrganizationPermissions.Organizations.RemoveUser)]
    Task RemoveUsers(long id, long[] userIds);

    /// <summary>
    /// 获取组织机构树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [GetCachingIntercept("OrganizationTree", OnlyCurrentUserData = true)]
    Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync();

    /// <summary>
    /// 设置某个组织机构可分配的角色权限
    /// </summary>
    /// <param name="id"></param>
    /// <param name="roleIds"></param>
    /// <returns></returns>
    [HttpPut("{id:long}/role")]
    [Authorize(OrganizationPermissions.Organizations.AllocationRole)]
    [RemoveCachingIntercept(typeof(GetOrganizationOutput), "id:{id}:*")]
    Task SetAllocationRoleListAsync(long id, long[] roleIds);


    /// <summary>
    /// 设置某个组织机构可分配的职位
    /// </summary>
    /// <param name="id"></param>
    /// <param name="positionIds"></param>
    /// <returns></returns>
    [HttpPut("{id:long}/position")]
    [Authorize(OrganizationPermissions.Organizations.AllocationPosition)]
    [RemoveCachingIntercept(typeof(ICollection<GetPositionOutput>), "getOrganizationPositionList:{id}")]
    [RemoveCachingIntercept(typeof(long[]), "getOrganizationPositionIds:{id}")]
    [RemoveCachingIntercept(typeof(GetOrganizationOutput), "id:{id}:*")]
    Task SetAllocationPositionListAsync(long id, long[] positionIds);

    /// <summary>
    /// 获取当前登陆用户所拥有的组织机构
    /// </summary>
    /// <returns></returns>
    [HttpGet("currentuser/list")]
    Task<ICollection<GetOrganizationOutput>> GetCurrentOrganizationListAsync();

    ///  <summary>
    /// 检查某个部门是否存在领导
    ///  </summary>
    ///  <param name="organizationId"></param>
    ///  <param name="userId"></param>
    ///  <returns></returns>
    [HttpPost("{organizationId:long}/checkleader")]
    Task<bool> CheckHasLeaderAsync(long organizationId, [FromQuery] long? userId);

    /// <summary>
    /// 判断是否存在组织机构
    /// </summary>
    /// <param name="organizationId">组织机构id</param>
    /// <returns></returns>
    [ProhibitExtranet]
    [GetCachingIntercept("HasOrganization:{organizationId}")]
    Task<bool> HasOrganizationAsync(long organizationId);

    /// <summary>
    /// 获取自身和孩子节点的组织机构id
    /// </summary>
    /// <param name="organizationId"></param>
    /// <returns></returns>
    [ProhibitExtranet]
    [GetCachingIntercept("GetSelfAndChildrenOrganizationIds:{organizationId}")]
    Task<ICollection<long>> GetSelfAndChildrenOrganizationIdsAsync(long organizationId);

    /// <summary>
    /// 获取用于分配组织机构的角色列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("role/allocation/list")]
    [Authorize(OrganizationPermissions.Organizations.AllocationRole)]
    [GetCachingIntercept("allocationOrganizationRoleList")]
    Task<ICollection<GetRoleOutput>> GetAllocationRoleListAsync();

    /// <summary>
    /// 获取用于分配组织机构职位列表
    /// </summary>
    /// <returns></returns>
    [HttpGet("position/allocation/list")]
    [Authorize(OrganizationPermissions.Organizations.AllocationPosition)]
    [GetCachingIntercept("allocationOrganizationPositionList")]
    Task<ICollection<GetPositionOutput>> GetAllocationPositionListAsync();

    [ProhibitExtranet]
    Task<long[]> GetOrganizationRoleIdsAsync(long[] organizationIds);

    [ProhibitExtranet]
    [GetCachingIntercept("getOrganizationPositionIds:{organizationId}")]
    Task<long[]> GetOrganizationPositionIdsAsync(long organizationId);
}