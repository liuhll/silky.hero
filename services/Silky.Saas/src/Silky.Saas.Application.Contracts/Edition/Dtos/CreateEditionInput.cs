using System.Collections.Generic;

namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public class CreateEditionInput : EditionDtoBase
{
    public ICollection<EditionFeatureDto> EditionFeatures { get; set; }
}