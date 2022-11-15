using System;
using System.Collections.Generic;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Extras.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Saas.Domain;

public class FeatureCatalog : Entity<long>, ICreatedObject, IUpdatedObject, ISoftDeletedObject
{
    public string Name { get; set; }
    
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
    
    public virtual ICollection<Feature> Features { get; set; }
}