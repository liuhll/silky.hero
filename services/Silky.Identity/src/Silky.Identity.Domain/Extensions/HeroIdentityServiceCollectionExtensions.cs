using System;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class HeroIdentityServiceCollectionExtensions
{
    public static IdentityBuilder AddHeroIdentity(this IServiceCollection services)
    {
        return services.AddHeroIdentity(setupAction: null);
    }

    public static IdentityBuilder AddHeroIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction)
    {
        return null;
    }
}