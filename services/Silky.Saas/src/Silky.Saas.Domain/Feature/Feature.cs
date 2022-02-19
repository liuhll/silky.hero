using System;
using System.Collections.Generic;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Saas.Domain;

public class Feature : Entity<long>, ICreatedObject, IUpdatedObject, ISoftDeletedObject
{

    public Feature()
    {
        Metas = new Dictionary<string, object>();
    }

    public long FeatureCatalogId { get; set; }
    public string Name { get; set; }

    public string Code { get; set; }

    public FeatureType FeatureType { get; set; }

    public string Description { get; set; }

    public IDictionary<string, object> Metas { get; set; }

    public virtual FeatureCatalog FeatureCatalog { get; set; }

    public virtual ICollection<EditionFeature> EditionFeatures { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
}