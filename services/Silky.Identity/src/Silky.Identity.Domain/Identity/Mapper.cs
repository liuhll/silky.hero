using System.Linq;
using Mapster;
using Silky.Identity.Application.Contracts.Role.Dtos;

namespace Silky.Identity.Domain;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<IdentityRole, GetRoleMenuOutput>()
            .AfterMapping((src, dest) =>
            {
                dest.MenuIds = src.Menus.Select(p => p.MenuId).ToArray();
            });
    }
}