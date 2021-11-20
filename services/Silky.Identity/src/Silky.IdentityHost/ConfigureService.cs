using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silky.Identity.EntityFrameworkCore.DbContexts;

namespace Silky.IdentityHost
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
                "Silky.Identity.Database.Migrations");
        }
    }
}