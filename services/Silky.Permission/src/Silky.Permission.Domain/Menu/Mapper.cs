using Mapster;
using Silky.Permission.Application.Contracts.Menu.Dtos;
using Silky.Permission.Domain.Shared.Menu;

namespace Silky.Permission.Domain.Menu;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateOrUpdateMenuInput, Menu>()
            .AfterMapping(m => { m.Type = MenuType.Menu; });
        config.ForType<CreateOrUpdateCatalogInput, Menu>()
            .AfterMapping(m => { m.Type = MenuType.Catalog; });
    }
}