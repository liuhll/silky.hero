using System.Collections.Generic;

namespace Silky.Saas.Application.Contracts.Feature.Dtos;

public class GetFeatureCatalogOutput
{
    public string Name { get; set; }

    public ICollection<GetFeatureOutput> Features { get; set; }
}