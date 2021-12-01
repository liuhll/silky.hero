using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Identity.Application.Contracts.User;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain.Organizations;

public class OrganizationDomainService : IOrganizationDomainService
{
    public IRepository<Organization> OrganizationRepository { get; }
    private readonly IUserAppService _userAppService;

    public OrganizationDomainService(IRepository<Organization> organizationRepository,
        IUserAppService userAppService)
    {
        OrganizationRepository = organizationRepository;
        _userAppService = userAppService;
    }

    public async Task CreateAsync(CreateOrUpdateOrganizationInput input)
    {
        var exsitOrganization = await OrganizationRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (exsitOrganization != null)
        {
            throw new UserFriendlyException($"已经存在名称为{input.Name}的机构");
        }

        exsitOrganization = await OrganizationRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
        if (exsitOrganization != null)
        {
            throw new UserFriendlyException($"已经存在Code为{input.Code}的机构");
        }

        var organization = input.Adapt<Organization>();
        await OrganizationRepository.InsertAsync(organization);
    }

    public async Task UpdateAsync(CreateOrUpdateOrganizationInput input)
    {
        Check.NotNull(input.Id, nameof(input.Id));
        var organization = await OrganizationRepository.FindOrDefaultAsync(input.Id.Value);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}的机构");
        }

        if (!input.Name.Equals(organization.Name))
        {
            var exsitOrganization = await OrganizationRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (exsitOrganization != null)
            {
                throw new UserFriendlyException($"已经存在名称为{input.Name}的机构");
            }
        }

        if (!input.Code.Equals(organization.Code))
        {
            var exsitOrganization = await OrganizationRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
            if (exsitOrganization != null)
            {
                throw new UserFriendlyException($"已经存在Code为{input.Code}的机构");
            }
        }

        if (input.ParentId.HasValue)
        {
            if (input.ParentId.Value == input.Id.Value)
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

    public async Task DeleteAsync(long id)
    {
        var organization = await OrganizationRepository.Include(p => p.Children).FirstOrDefaultAsync(p => p.Id == id);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的机构");
        }

        if (organization.Children.Any())
        {
            throw new UserFriendlyException($"请先删除下属机构");
        }

        var orgUsers = await _userAppService.GetOrganizationUsersAsync(id);
        if (orgUsers.Any())
        {
            throw new UserFriendlyException($"该机构存在用户,无法删除");
        }

        await OrganizationRepository.DeleteAsync(organization);
    }

    public async Task<ICollection<GetOrganizationTreeOutput>> GetTreeAsync()
    {
        var organizations = await OrganizationRepository.AsQueryable()
            .OrderByDescending(p=> p.Sort)
            .ProjectToType<GetOrganizationTreeOutput>().ToListAsync();
        return organizations.BuildTree();
    }
}