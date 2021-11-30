using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silky.Organization.EntityFrameworkCore.DbContexts;

namespace Silky.OrganizationHost
{
    public class ConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSilkyCaching()
                .AddSilkySkyApm()
                .AddMessagePackCodec();
            
            services.AddDatabaseAccessor(
                options => { options.AddDbPool<DefaultDbContext>(); },
                "Silky.Organization.Database.Migrations");
        }
    }
}