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
                options => {
#if NET7_0
                    options.AddDbPool<DefaultDbContext>();
#else
                    options.AddDbPool<DefaultDbContext>(default, opts => { opts.UseBatchEF_MySQLPomelo(); });
#endif
                },
                "Silky.Organization.Database.Migrations");
        }
    }
}