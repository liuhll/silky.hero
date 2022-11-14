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
                    options.AddDbPool<DefaultDbContext>(default, opts => { opts.UseBatchEF_MySQLPomelo(); });
                },
                "Silky.Identity.Database.Migrations");
        }
    }
}