using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Saas.Domain;

public class EditionFeature : Entity<long>, ICreatedObject, IUpdatedObject
{
    public EditionFeature()
    {
    }
    
    public EditionFeature(long editionId,long featureId,int featureValue)
    {
        EditionId = editionId;
        FeatureId = featureId;
        FeatureValue = featureValue;
    }

    public long EditionId { get; set; }
    
    public long FeatureId { get; set; }

    public int FeatureValue { get; set; }

    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    
    public Edition Edition { get; set; }
    
    public Feature Feature { get; set; }
}