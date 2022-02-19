using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
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

    public async Task<PagedList<GetEditionPageOutput>> GetPageAsync(GetEditionPageInput input)
    {
        var pageOutputs = await _editionDomainService.EditionRepository
            .AsQueryable(false)
            .Where(!input.Name.IsNullOrEmpty(), p => p.Name.Contains(input.Name))
            .ProjectToType<GetEditionPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return pageOutputs;
    }

    public Task<GetEditionEditOutput> GetAsync(long id)
    {
        return _editionDomainService.GetAsync(id);
    }
}