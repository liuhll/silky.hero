using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silky.Core.Modularity;
using Silky.Saas.EntityFrameworkCore.DbContexts;

namespace Silky.Saas.EntityFrameworkCore;

public class SaasEfCoreModule : SilkyModule
{
    public override async Task Initialize(ApplicationInitializationContext context)
    {
        if (context.HostEnvironment.IsDevelopment())
        {
            using var scope = context.ServiceProvider.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}