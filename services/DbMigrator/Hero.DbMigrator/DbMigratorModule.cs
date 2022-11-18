using System.Threading.Tasks;
using Silky.Core.Modularity;

namespace Hero.DbMigrator;

public class DbMigratorModule : SilkyModule
{
    public override Task Initialize(ApplicationInitializationContext context)
    {
        return Task.CompletedTask;
    }
}