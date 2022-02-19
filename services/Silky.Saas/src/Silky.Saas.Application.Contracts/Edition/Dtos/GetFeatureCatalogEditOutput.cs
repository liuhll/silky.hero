using System.Collections.Generic;

namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public class GetFeatureCatalogEditOutput
{
    public string Name { get; set; }

    public ICollection<GetFeatureEditOutput> Features { get; set; }
}