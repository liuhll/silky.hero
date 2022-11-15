using System;
using System.Collections.Generic;
using System.Linq;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Extras.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Saas.Domain;

public class Edition : Entity<long>, ICreatedObject, IUpdatedObject, ISoftDeletedObject
{
    public Edition()
    {
        Tenants = new List<Tenant>();
        EditionFeatures = new List<EditionFeature>();
    }

    public Edition(string name, decimal? price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; set; }
    public decimal? Price { get; set; }
    
    public int Sort { get; set; }
    
    public string Remark { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }

    public void SetEditionFeature(long featureId, int featureValue)
    {
        if (EditionFeatures.Any(p => p.FeatureId == featureId && p.EditionId == Id))
        {
            EditionFeatures.Single(p => p.FeatureId == featureId && p.EditionId == Id).FeatureValue = featureValue;
        }
        else
        {
            EditionFeatures.Add(new EditionFeature(Id, featureId, featureValue));
        }
    }

    public ICollection<EditionFeature> EditionFeatures { get; internal protected set; }

    public ICollection<Tenant> Tenants { get; internal protected set; }
}