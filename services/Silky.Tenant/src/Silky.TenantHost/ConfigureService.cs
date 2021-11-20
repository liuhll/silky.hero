using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silky.Tenant.EntityFrameworkCore.DbContexts;

namespace Silky.TenantHost
{
    public class ConfigureService : IConfigureService
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSilkyCaching()
                .AddSilkySkyApm()
                .AddMessagePackCodec();
            
            services.AddDatabaseAccessor(
                options => { options.AddDbPool<DefaultContext>(); },
                "Silky.Tenant.Database.Migrations");
        }
    }
}