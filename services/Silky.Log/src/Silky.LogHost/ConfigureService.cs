using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silky.Log.EntityFrameworkCore.DbContexts;

namespace Silky.LogHost
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
                "Silky.Log.Database.Migrations");
        }
    }
}