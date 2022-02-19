using System.Collections.Generic;

namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public class UpdateEditionInput : EditionDtoBase
{
    public long Id { get; set; }
    
    public ICollection<EditionFeatureDto> EditionFeatures { get; set; }
}