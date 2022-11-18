using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silky.Core.Modularity;
using Silky.Identity.EntityFrameworkCore.DbContexts;

namespace Silky.Identity.EntityFrameworkCore;

public class IdentityEfCoreModule : SilkyModule
{
    public override async Task Initialize(ApplicationInitializationContext context)
    {
        if (context.HostEnvironment.IsDevelopment())
        {
            using var scope = context.ServiceProvider.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}