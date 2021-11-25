using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Silky.Identity.Domain;

public class HeroUserClaimsPrincipalFactory :  UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
{
    public HeroUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options) 
        : base(userManager,
            roleManager,
            options)
    {
    }
}