using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Silky.Core;
using Silky.Core.Configuration;

namespace Hero.DbMigrator;

internal static class EngineHelper
{
    internal static IEngine CreateEngine()
    {
        var services = new ServiceCollection();
        var configuration = CreateConfigurationBuilder().Build();
        var hostEnvironment = CreateHostEnvironment();
        services.AddSingleton(configuration);
        services.AddSingleton(hostEnvironment);

        var engine = services.AddSilkyServices<DbMigratorModule>(configuration, hostEnvironment,
            new SilkyApplicationCreationOptions());
        var containerBuilder = new ContainerBuilder();

        containerBuilder.Populate(services);

        engine.RegisterDependencies(containerBuilder);
        engine.RegisterModules(containerBuilder);
        var container = containerBuilder.Build();
        engine.ServiceProvider = new AutofacServiceProvider(container);
        return engine;
    }

    private static IConfigurationBuilder CreateConfigurationBuilder()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory());
    }

    private static IHostEnvironment CreateHostEnvironment()
    {
        var mockEnvironment = new Mock<IHostEnvironment>();
        mockEnvironment.Setup(e => e.EnvironmentName)
            .Returns("Development");
        mockEnvironment.Setup(e => e.ApplicationName)
            .Returns("HeroDbMigrator");
        mockEnvironment.Setup(m => m.ContentRootPath)
            .Returns(Directory.GetCurrentDirectory());
        return mockEnvironment.Object;
    }
}