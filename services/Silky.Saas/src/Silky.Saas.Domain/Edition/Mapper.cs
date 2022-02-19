using Mapster;
using Silky.Saas.Application.Contracts.Edition.Dtos;

namespace Silky.Saas.Domain;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Feature, GetFeatureEditOutput>()
            .Map(dest => dest.FeatureId, src => src.Id);
    }
}