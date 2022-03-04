using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Caching;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.Extensions;
using Silky.Saas.Application.Contracts.Edition.Dtos;

namespace Silky.Saas.Domain;

public class EditionDomainService : IScopedDependency, IEditionDomainService
{
    public IRepository<Edition> EditionRepository { get; }
    public IRepository<EditionFeature> EditionFeatureRepository { get; }
    private readonly IRepository<Feature> _featureRepository;
    private readonly IRepository<FeatureCatalog> _featureCatalogRepository;
    private readonly IRepository<Tenant> _tenantRepository;
    private readonly IDistributedCache _cache;
    private readonly IDistributedCacheKeyNormalizer _distributedCacheKeyNormalizer;
    private readonly ISession _session;

    public EditionDomainService(IRepository<Edition> editionRepository,
        IRepository<FeatureCatalog> featureCatalogRepository,
        IRepository<EditionFeature> editionFeatureRepository,
        IRepository<Tenant> tenantRepository,
        IRepository<Feature> featureRepository,
        IDistributedCache cache,
        IDistributedCacheKeyNormalizer distributedCacheKeyNormalizer
    )
    {
        EditionRepository = editionRepository;
        EditionFeatureRepository = editionFeatureRepository;
        _tenantRepository = tenantRepository;
        _cache = cache;
        _distributedCacheKeyNormalizer = distributedCacheKeyNormalizer;
        _featureRepository = featureRepository;
        _featureCatalogRepository = featureCatalogRepository;
        _session = NullSession.Instance;
    }

    public async Task CreateAsync(CreateEditionInput input)
    {
        var edition = await EditionRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (edition != null)
        {
            throw new BusinessException($"已经存在{input.Name}的版本");
        }

        edition = input.Adapt<Edition>();
        await EditionRepository.InsertAsync(edition);
    }

    public async Task UpdateAsync(UpdateEditionInput input)
    {
        var edition = await EditionRepository.FirstOrDefaultAsync(p => p.Id == input.Id);
        if (edition == null)
        {
            throw new BusinessException($"不存在Id为{input.Id}的版本");
        }

        edition = input.Adapt(edition);
        await EditionRepository.UpdateAsync(edition);
        await RemoveEditionFeatureCache(edition.Id);
    }

    public async Task DeleteAsync(long id)
    {
        var edition = await EditionRepository
            .Include(p => p.Tenants)
            .Include(p => p.EditionFeatures)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (edition == null)
        {
            throw new BusinessException($"不存在Id为{id}的版本");
        }

        if (edition.Tenants.Any())
        {
            throw new BusinessException($"无法删除已经被分配给租户的版本");
        }

        await EditionRepository.DeleteAsync(edition);
        await RemoveEditionFeatureCache(edition.Id);
    }

    public async Task<GetEditionEditOutput> GetAsync(long id)
    {
        var edition = await EditionRepository
            .Include(p => p.EditionFeatures)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (edition == null)
        {
            throw new BusinessException($"不存在Id为{id}的版本");
        }

        var featureCatalogs = await _featureCatalogRepository
            .AsQueryable(false)
            .Include(p => p.Features)
            .ToListAsync();
        var output = edition.Adapt<GetEditionEditOutput>();
        output.FeatureCatalogs = SetFeatureCatalogs(featureCatalogs, edition.EditionFeatures);
        return output;
    }

    public async Task SetFeaturesAsync(long id, ICollection<EditionFeatureDto> editionFeatures)
    {
        var edition = await EditionRepository.Include(p => p.EditionFeatures).FirstOrDefaultAsync(p => p.Id == id);
        if (edition == null)
        {
            throw new BusinessException($"不存在Id为{id}的版本");
        }

        foreach (var editionFeature in editionFeatures)
        {
            edition.SetEditionFeature(editionFeature.FeatureId, editionFeature.FeatureValue);
        }

        await EditionRepository.UpdateAsync(edition);
        await RemoveEditionFeatureCache(edition.Id);
    }

    public async Task<GetEditionFeatureOutput> GetEditionFeatureAsync(string featureCode, long? tenantId = null)
    {
        var tId = tenantId.HasValue ? tenantId.Value : _session.TenantId.To<long>();
        var tenant = await _tenantRepository.FindAsync(tId);
        var editionFeature =
            await EditionFeatureRepository
                .AsQueryable(false)
                .Include(p => p.Feature)
                .FirstOrDefaultAsync(p =>
                    p.EditionId == tenant.EditionId && p.Feature.Code == featureCode);
        if (editionFeature != null)
        {
            var editionFeatureOutput = new GetEditionFeatureOutput()
            {
                FeatureId = editionFeature.Feature.Id,
                Code = editionFeature.Feature.Code,
                FeatureValue = editionFeature.FeatureValue
            };
            return editionFeatureOutput;
        }

        var feature = await _featureRepository
            .AsQueryable(false)
            .FirstAsync(p => p.Code == featureCode);
        var defaultEditionFeatureOutput = new GetEditionFeatureOutput()
        {
            FeatureId = feature.Id,
            Code = feature.Code,
            FeatureValue = feature.DefaultValue
        };
        return defaultEditionFeatureOutput;
    }

    private async Task RemoveEditionFeatureCache(long editionId)
    {
        await _cache.RemoveMatchKeyAsync("*CurrentUserMenus:*");
        await _cache.RemoveMatchKeyAsync("*CurrentUserPermissionCodes:*");
        var editionTenants =
            await _tenantRepository.AsQueryable(false).Where(p => p.EditionId == editionId).ToArrayAsync();
        foreach (var tenant in editionTenants)
        {
            await _cache.RemoveMatchKeyAsync(typeof(GetEditionFeatureOutput),
                _distributedCacheKeyNormalizer.NormalizeTenantKey("featureCode:*",
                    typeof(GetEditionFeatureOutput).FullName, tenant.Id));
        }
    }


    private ICollection<GetFeatureCatalogEditOutput> SetFeatureCatalogs(List<FeatureCatalog> featureCatalogs,
        ICollection<EditionFeature> editionEditionFeatures)
    {
        var featureCatalogOutputs = featureCatalogs.Adapt<ICollection<GetFeatureCatalogEditOutput>>();
        foreach (var featureCatalogOutput in featureCatalogOutputs)
        {
            foreach (var featureEditOutput in featureCatalogOutput.Features)
            {
                var editionEditionFeature =
                    editionEditionFeatures.FirstOrDefault(p => p.FeatureId == featureEditOutput.FeatureId);
                featureEditOutput.FeatureValue = editionEditionFeature?.FeatureValue ?? 0;
            }
        }

        return featureCatalogOutputs;
    }
}