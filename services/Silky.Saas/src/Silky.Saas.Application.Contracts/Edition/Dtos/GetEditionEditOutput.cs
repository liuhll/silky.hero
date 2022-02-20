using System.Collections.Generic;

namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public class GetEditionEditOutput 
{
    public long Id { get; set; }

    public string Name { get; set; }
    
    public ICollection<GetFeatureCatalogEditOutput> FeatureCatalogs { get; set; }
}