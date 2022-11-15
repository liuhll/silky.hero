using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silky.Organization.EntityFrameworkCore.DbContexts;

namespace Silky.OrganizationHost
{
    public class ConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSilkySkyApm()
                .AddObjectMapper();
            
            services.AddDatabaseAccessor(
                options => { options.AddDbPool<DefaultDbContext>(providerName: default, optionBuilder: opt =>
                {
                    opt.UseBatchEF_MySQLPomelo();
                }); },
                "Silky.Organization.Database.Migrations");
        }
    }
}