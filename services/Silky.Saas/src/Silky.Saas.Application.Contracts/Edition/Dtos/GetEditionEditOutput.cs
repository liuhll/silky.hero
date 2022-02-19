using System.Collections.Generic;

namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public class GetEditionEditOutput : EditionFeatureDto
{
    public ICollection<GetFeatureCatalogEditOutput> FeatureCatalogs { get; set; }
}