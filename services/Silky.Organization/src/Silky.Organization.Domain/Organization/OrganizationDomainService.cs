using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Core.Exceptions;
using Silky.Core.Runtime.Rpc;
using Silky.Core.Serialization;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Identity.Application.Contracts.User;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain;

public class OrganizationDomainService : IOrganizationDomainService
{
    public IRepository<Organization> OrganizationRepository { get; }
    private readonly IUserAppService _userAppService;
    private readonly ISerializer _serializer;

    public OrganizationDomainService(IRepository<Organization> organizationRepository,
        IUserAppService userAppService,
        ISerializer serializer)
    {
        OrganizationRepository = organizationRepository;
        _userAppService = userAppService;
        _serializer = serializer;
    }

    public async Task CreateAsync(CreateOrganizationInput input)
    {
        var exsitOrganization =
            await OrganizationRepository.FirstOrDefaultAsync(p => p.Name == input.Name && p.ParentId == input.ParentId);
        if (exsitOrganization != null)
        {
            throw new UserFriendlyException($"已经存在名称为{input.Name}的机构");
        }

        var organization = input.Adapt<Organization>();
        await OrganizationRepository.InsertAsync(organization);
    }

    public async Task UpdateAsync(UpdateOrganizationInput input)
    {
        var organization = await OrganizationRepository.FindOrDefaultAsync(input.Id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}的机构");
        }

        if (!input.Name.Equals(organization.Name))
        {
            var exsitOrganization =
                await OrganizationRepository.FirstOrDefaultAsync(p =>
                    p.Name == input.Name && p.ParentId == input.ParentId);
            if (exsitOrganization != null)
            {
                throw new UserFriendlyException($"已经存在名称为{input.Name}的机构");
            }
        }

        if (input.ParentId.HasValue)
        {
            if (input.ParentId.Value == input.Id)
            {
                throw new UserFriendlyException($"父节点不允许为本节点");
            }

            var parent = await OrganizationRepository.FindOrDefaultAsync(input.ParentId.Value);
            if (parent == null)
            {
                throw new UserFriendlyException($"不存在{input.ParentId}的机构");
            }

            // var children = await _organizationRepository
            //     .Include(p => p.Children)
            //     .Where(p => p.ParentId == input.Id).ToListAsync();
            // var allChildren = children
            //     .SelectManyRecursive(p => p.Children);
            // if (allChildren.Any(p => p.Id == input.ParentId))
            // {
            //     throw new UserFriendlyException("父节点不能为本节点的子节点，请重新选择父节点");
            // }
        }

        organization = input.Adapt(organization);
        await OrganizationRepository.UpdateAsync(organization);
    }


    public async Task DeleteTryAsync(long id)
    {
        var organization = await OrganizationRepository.FirstOrDefaultAsync(p => p.Id == id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的机构");
        }

        var organizationAndChildrenIds =
            (await GetChildrenOrganizationsAsync(id)).ToArray().Select(p => p.Id).ToArray();
        RpcContext.Context.SetInvokeAttachment("organizationAndChildrenIds",
            _serializer.Serialize(organizationAndChildrenIds));
        await _userAppService.RemoveOrganizationLinkedDataAsync(organizationAndChildrenIds);
    }

    [UnitOfWork]
    public async Task DeleteConfirmAsync(long id)
    {
        var organizationAndChildrenIds =
            _serializer.Deserialize<long[]>(RpcContext.Context.GetInvokeAttachment("organizationAndChildrenIds")
                .ToString());
        var organizations = await OrganizationRepository.Where(p => organizationAndChildrenIds.Contains(p.Id))
            .ToArrayAsync();
        await OrganizationRepository.DeleteAsync(organizations);
    }

    public async Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync()
    {
        var organizations = await OrganizationRepository.AsQueryable(false)
            .OrderByDescending(p => p.Sort)
            .ProjectToType<GetOrganizationTreeOutput>().ToListAsync();
        return organizations.BuildTree();
    }

    public async Task<IEnumerable<Organization>> GetChildrenOrganizationsAsync(long organizationId,
        bool includeSelf = true)
    {
        var organizations = await OrganizationRepository.AsQueryable(false)
            .OrderByDescending(p => p.Sort).ToListAsync();
        return organizations.GetChildrenOrganizations(organizationId, includeSelf);
    }

    public async Task SetAllocationRoleListAsync(long id, long[] roleIds)
    {
        var organization = await OrganizationRepository
            .Include(p => p.OrganizationRoles)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的机构");
        }

        organization.OrganizationRoles.Clear();
        organization.SetRoles(roleIds);
        await OrganizationRepository.UpdateAsync(organization);
    }
}