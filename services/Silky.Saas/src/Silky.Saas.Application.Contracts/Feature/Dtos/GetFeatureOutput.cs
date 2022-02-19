using System.Collections.Generic;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Saas.Application.Contracts.Feature.Dtos;

public class GetFeatureOutput
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public FeatureType FeatureType { get; set; }
    
    public string Description { get; set; }

    public IDictionary<string, object> Metas { get; set; }
}