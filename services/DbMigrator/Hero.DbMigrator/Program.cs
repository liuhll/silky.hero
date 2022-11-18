using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Silky.Core.Modularity;

namespace Hero.DbMigrator;

public class Program
{
    public static async Task Main(string[] args)
    {
        var engine = EngineHelper.CreateEngine();
        using var scope = engine.ServiceProvider.CreateScope();
        var moduleManager = scope.ServiceProvider.GetRequiredService<IModuleManager>();
        await moduleManager.PreInitializeModules();
        await moduleManager.InitializeModules();
        await moduleManager.PostInitializeModules();
    }
}