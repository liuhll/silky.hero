using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain.Organizations;

public class OrganizationDomainService : IOrganizationDomainService
{
    private readonly IRepository<Organization> _organizationRepository;

    public OrganizationDomainService(IRepository<Organization> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task CreateAsync(CreateOrUpdateOrganizationInput input)
    {
        var exsitOrganization = await _organizationRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (exsitOrganization != null)
        {
            throw new UserFriendlyException($"已经存在名称为{input.Name}的机构");
        }

        exsitOrganization = await _organizationRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
        if (exsitOrganization != null)
        {
            throw new UserFriendlyException($"已经存在Code为{input.Code}的机构");
        }

        var organization = input.Adapt<Organization>();
        await _organizationRepository.InsertAsync(organization);
    }

    public async Task UpdateAsync(CreateOrUpdateOrganizationInput input)
    {
        Check.NotNull(input.Id, nameof(input.Id));
        var organization = await _organizationRepository.FindOrDefaultAsync(input.Id.Value);
        if (organization == null)
        {
            throw new UserFriendlyException($"不存在Id为{input.Id}的机构");
        }

        if (!input.Name.Equals(organization.Name))
        {
            var exsitOrganization = await _organizationRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (exsitOrganization != null)
            {
                throw new UserFriendlyException($"已经存在名称为{input.Name}的机构");
            }
        }

        if (!input.Code.Equals(organization.Code))
        {
            var exsitOrganization = await _organizationRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
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

            var parent = await _organizationRepository.FindOrDefaultAsync(input.ParentId.Value);
            if (parent == null)
            {
                throw new UserFriendlyException($"不存在{input.ParentId}的机构");
            }

            var children = await _organizationRepository
                .Include(p => p.Children)
                .Where(p => p.ParentId == input.Id).ToListAsync();
            var allChildren = children
                .SelectManyRecursive(p => p.Children);
            if (allChildren.Any(p => p.Id == input.ParentId))
            {
                throw new UserFriendlyException("父节点不能为本节点的子节点，请重新选择父节点");
            }
        }

        organization = input.Adapt(organization);
        await _organizationRepository.UpdateAsync(organization);
    }
}