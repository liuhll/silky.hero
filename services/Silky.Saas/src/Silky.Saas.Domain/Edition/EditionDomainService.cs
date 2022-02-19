using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Saas.Application.Contracts.Edition.Dtos;

namespace Silky.Saas.Domain;

public class EditionDomainService : IScopedDependency, IEditionDomainService
{
    public IRepository<Edition> EditionRepository { get; }
    private readonly IRepository<FeatureCatalog> _featureCatalogRepository;

    public EditionDomainService(IRepository<Edition> editionRepository, 
        IRepository<FeatureCatalog> featureCatalogRepository)
    {
        EditionRepository = editionRepository;
        _featureCatalogRepository = featureCatalogRepository;
    }

    public async Task CreateAsync(CreateEditionInput input)
    {
        var edition = await EditionRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (edition != null)
        {
            throw new BusinessException($"已经存在{input.Name}的版本");
        }

        edition = new Edition(input.Name, input.Price);
        foreach (var editionFeature in input.EditionFeatures)
        {
            edition.SetEditionFeature(editionFeature.FeatureId,editionFeature.FeatureValue);
        }

        await EditionRepository.InsertAsync(edition);
    }

    public async Task UpdateAsync(UpdateEditionInput input)
    {
        var edition = await EditionRepository.Include(p=> p.EditionFeatures).FirstOrDefaultAsync(p=> p.Id == input.Id);
        if (edition == null)
        {
            throw new BusinessException($"不存在Id为{input.Id}的版本");
        }

        edition.Name = input.Name;
        edition.Price = input.Price;
        foreach (var editionFeature in input.EditionFeatures)
        {
            edition.SetEditionFeature(editionFeature.FeatureId,editionFeature.FeatureValue);
        }
        await EditionRepository.UpdateAsync(edition);
    }

    public async Task DeleteAsync(long id)
    {
        var edition = await EditionRepository
            .Include(p=> p.Tenants)
            .Include(p=> p.EditionFeatures)
            .FirstOrDefaultAsync(p=> p.Id == id);
        if (edition == null)
        {
            throw new BusinessException($"不存在Id为{id}的版本");
        }

        if (edition.Tenants.Any())
        {
            throw new BusinessException($"无法删除已经被分配给租户的版本");
        }

        await EditionRepository.DeleteAsync(edition);

    }

    public async Task<GetEditionEditOutput> GetAsync(long id)
    {
        var edition = await EditionRepository
            .Include(p=> p.EditionFeatures)
            .FirstOrDefaultAsync(p=> p.Id == id);
        if (edition == null)
        {
            throw new BusinessException($"不存在Id为{id}的版本");
        }

        var featureCatalogs = await _featureCatalogRepository
            .AsQueryable(false)
            .Include(p=> p.Features)
            .ToListAsync();
        var output = edition.Adapt<GetEditionEditOutput>();
        output.FeatureCatalogs = SetFeatureCatalogs(featureCatalogs, edition.EditionFeatures);
        return output;
    }

    private ICollection<GetFeatureCatalogEditOutput> SetFeatureCatalogs(List<FeatureCatalog> featureCatalogs, ICollection<EditionFeature> editionEditionFeatures)
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