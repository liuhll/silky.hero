using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Application.Contracts.Edition.Dtos;
using Silky.Saas.Domain;

namespace Silky.Saas.Application.Edition;

public class EditionAppService : IEditionAppService
{
    private readonly IEditionDomainService _editionDomainService;

    public EditionAppService(IEditionDomainService editionDomainService)
    {
        _editionDomainService = editionDomainService;
    }

    [UnitOfWork]
    public Task CreateAsync(CreateEditionInput input)
    {
        return _editionDomainService.CreateAsync(input);
    }

    [UnitOfWork]
    public Task UpdateAsync(UpdateEditionInput input)
    {
        return _editionDomainService.UpdateAsync(input);
    }

    public Task DeleteAsync(long id)
    {
        return _editionDomainService.DeleteAsync(id);
    }

    public Task<bool> CheckAsync(CheckEditionInput input)
    {
        return _editionDomainService.EditionRepository.AnyAsync(p => p.Name == input.Name && p.Id != input.Id, false);
    }

    public async Task<PagedList<GetEditionPageOutput>> GetPageAsync(GetEditionPageInput input)
    {
        var pageOutputs = await _editionDomainService.EditionRepository
            .AsQueryable(false)
            .Where(!input.Name.IsNullOrEmpty(), p => p.Name.Contains(input.Name))
            .OrderByDescending(p => p.Sort)
            .ProjectToType<GetEditionPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return pageOutputs;
    }

    public Task SetFeaturesAsync(long id, ICollection<EditionFeatureDto> inputs)
    {
        return _editionDomainService.SetFeaturesAsync(id, inputs);
    }

    public Task<GetEditionEditOutput> GetAsync(long id)
    {
        return _editionDomainService.GetAsync(id);
    }

    public async Task<ICollection<GetEditionOutput>> GetListAsync()
    {
        return await _editionDomainService
            .EditionRepository
            .AsQueryable(false)
            .OrderByDescending(p => p.Sort)
            .ProjectToType<GetEditionOutput>()
            .ToListAsync();
    }

    public Task<GetEditionFeatureOutput> GetEditionFeatureAsync(string featureCode)
    {
        return _editionDomainService.GetEditionFeatureAsync(featureCode);
    }

    public Task<GetEditionFeatureOutput> GetTenantEditionFeatureAsync(string featureCode, long tenantId)
    {
        return _editionDomainService.GetEditionFeatureAsync(featureCode, tenantId);
    }
    
}