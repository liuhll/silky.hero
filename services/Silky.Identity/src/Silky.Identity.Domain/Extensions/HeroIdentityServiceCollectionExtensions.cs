using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Silky.Identity.Domain;
using IdentityRole = Silky.Identity.Domain.IdentityRole;
using IdentityUser = Silky.Identity.Domain.IdentityUser;

namespace Microsoft.Extensions.DependencyInjection;

public static class HeroIdentityServiceCollectionExtensions
{
    public static IdentityBuilder AddHeroIdentity(this IServiceCollection services)
    {
        return services.AddHeroIdentity(setupAction: null);
    }

    public static IdentityBuilder AddHeroIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction)
    {
        //RoleManager
        services.TryAddScoped<IdentityRoleManager>();
        services.TryAddScoped(typeof(RoleManager<IdentityRole>), provider => provider.GetService(typeof(IdentityRoleManager)));

        //UserManager
        services.TryAddScoped<IdentityUserManager>();
        services.TryAddScoped(typeof(UserManager<IdentityUser>), provider => provider.GetService(typeof(IdentityUserManager)));

        //UserStore
        services.TryAddScoped<IdentityUserStore>();
        services.TryAddScoped(typeof(IUserStore<IdentityUser>), provider => provider.GetService(typeof(IdentityUserStore)));

        //RoleStore
        services.TryAddScoped<IdentityRoleStore>();
        services.TryAddScoped(typeof(IRoleStore<IdentityRole>), provider => provider.GetService(typeof(IdentityRoleStore)));
        
        services.TryAddScoped<IdentityUserValidator>();
        services.TryAddScoped(typeof(IUserValidator<IdentityUser>), provider => provider.GetService(typeof(IdentityUserValidator)));

        return services
            .AddIdentityCore<IdentityUser>(setupAction)
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<HeroUserClaimsPrincipalFactory>();
    }
}