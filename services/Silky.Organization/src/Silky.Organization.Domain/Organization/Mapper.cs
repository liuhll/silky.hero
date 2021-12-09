using Mapster;
using Silky.Organization.Application.Contracts.Organization.Dtos;

namespace Silky.Organization.Domain;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Organization, GetOrganizationTreeOutput>()
            .Ignore(d=> d.Children);
    }
}