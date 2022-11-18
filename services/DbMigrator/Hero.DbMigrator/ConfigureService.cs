using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BasicDataDbContexts = Silky.BasicData.EntityFrameworkCore.DbContexts;
using IdentityDbContexts = Silky.Identity.EntityFrameworkCore.DbContexts;
using LogDbContexts = Silky.Log.EntityFrameworkCore.DbContexts;
using OrganizationDbContexts = Silky.Organization.EntityFrameworkCore.DbContexts;
using PermissionDbContexts = Silky.Permission.EntityFrameworkCore.DbContexts;
using PositionDbContexts = Silky.Position.EntityFrameworkCore.DbContexts;
using SaasDbContexts = Silky.Saas.EntityFrameworkCore.DbContexts;

namespace Hero.DbMigrator;

public class ConfigureService : IConfigureService
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(builder => { builder.AddConsole(); });
        services.AddDatabaseAccessor(
            options =>
            {
                options.AddDbPool<BasicDataDbContexts.DefaultContext>(DbProvider.MySql,
                    connectionString: configuration.GetConnectionString("basicData"));
            },
            "Silky.BasicData.Database.Migrations");        
        // services.AddDatabaseAccessor(
        //     options =>
        //     {
        //         options.AddDbPool<IdentityDbContexts.DefaultDbContext>(DbProvider.MySql,
        //             connectionString: configuration.GetConnectionString("identity"));
        //     },
        //     "Silky.Identity.Database.Migrations");
        //
        // services.AddDatabaseAccessor(
        //     options =>
        //     {
        //         options.AddDbPool<LogDbContexts.DefaultContext>(DbProvider.MySql,
        //             connectionString: configuration.GetConnectionString("log"));
        //     },
        //     "Silky.Log.Database.Migrations");
        //
        // services.AddDatabaseAccessor(
        //     options =>
        //     {
        //         options.AddDbPool<OrganizationDbContexts.DefaultDbContext>(DbProvider.MySql,
        //             connectionString: configuration.GetConnectionString("organization"));
        //     },
        //     "Silky.Organization.Database.Migrations");
        //
        // services.AddDatabaseAccessor(
        //     options =>
        //     {
        //         options.AddDbPool<PermissionDbContexts.DefaultContext>(DbProvider.MySql,
        //             connectionString: configuration.GetConnectionString("permission"));
        //     },
        //     "Silky.Permission.Database.Migrations");
        //
        // services.AddDatabaseAccessor(
        //     options =>
        //     {
        //         options.AddDbPool<PositionDbContexts.DefaultContext>(DbProvider.MySql,
        //             connectionString: configuration.GetConnectionString("position"));
        //     },
        //     "Silky.Position.Database.Migrations");
        //
        // services.AddDatabaseAccessor(
        //     options =>
        //     {
        //         options.AddDbPool<SaasDbContexts.DefaultContext>(DbProvider.MySql,
        //             connectionString: configuration.GetConnectionString("saas"));
        //     },
        //     "Silky.Saas.Database.Migrations");

        
    }
}