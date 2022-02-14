using System.Linq;
using Mapster;
using Silky.Identity.Application.Contracts.Role.Dtos;
using Silky.Identity.Domain.Identity;

namespace Silky.Identity.Domain;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<IdentityRole, GetRoleMenuOutput>()
            .AfterMapping((src, dest) => { dest.MenuIds = src.Menus.Select(p => p.MenuId).ToArray(); });
        config.ForType<IdentityRole, GetRoleDataRangeOutput>()
            .AfterMapping((src, dest) =>
            {
                dest.CustomOrganizationIds =
                    src.CustomOrganizationDataRanges.Select(p => p.OrganizationId).ToArray();
            });
        config.ForType<FrontendMenu, GetRoleMenuTreeOutput>()
            .Map(dest => dest.MenuId, src => src.Id)
            .Map(dest => dest.Title, src => src.Meta["Title"]);
    }
}