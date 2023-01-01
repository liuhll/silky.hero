using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silky.Identity.EntityFrameworkCore.DbContexts;

namespace Silky.IdentityHost
{
    public class ConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSilkySkyApm()
                .AddObjectMapper()
                .AddJwt();

            services.AddHeroIdentity(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            });

            services.AddDatabaseAccessor(
                options =>
                {
#if NET6
                     options.AddDbPool<DefaultDbContext>(default, opts => { opts.UseBatchEF_MySQLPomelo(); });
#elif NET7_0
                    options.AddDbPool<DefaultDbContext>();
#endif
                },
                "Silky.Identity.Database.Migrations");
        }
    }
}