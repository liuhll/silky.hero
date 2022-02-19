using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Saas.Application.Contracts.Feature;
using Silky.Saas.Application.Contracts.Feature.Dtos;

namespace Silky.Saas.Application.Feature;

public class FeatureAppService : IFeatureAppService
{
    private readonly IRepository<Domain.FeatureCatalog> _featureCatalogRepository;

    public FeatureAppService(IRepository<Domain.FeatureCatalog> featureCatalogRepository)
    {
        _featureCatalogRepository = featureCatalogRepository;
    }

    public async Task<ICollection<GetFeatureCatalogOutput>> GetFeaturesAsync()
    {
        var featureCatalogs = await _featureCatalogRepository
            .AsQueryable(false)
            .Include(p => p.Features)
            .ProjectToType<GetFeatureCatalogOutput>().ToListAsync();
        return featureCatalogs;
    }
}