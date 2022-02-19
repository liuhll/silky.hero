using System;
using System.Collections.Generic;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Saas.Domain;

public class Edition : Entity<long>, ICreatedObject, IUpdatedObject, ISoftDeletedObject
{
    public string Name { get; set; }
    public decimal? Price { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }

    public ICollection<EditionFeature> EditionFeatures { get; set; }

    public ICollection<Tenant> Tenants { get; set; }
}