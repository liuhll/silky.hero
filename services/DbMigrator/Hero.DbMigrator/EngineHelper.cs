using System;
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
    internal static IEngine CreateEngine(Action<SilkyApplicationCreationOptions> actionOption = null)
    {
        var services = new ServiceCollection();
        var options = new SilkyApplicationCreationOptions();
        actionOption?.Invoke(options);
        var hostEnvironment = CreateHostEnvironment();
        var configuration = CreateConfigurationBuilder(hostEnvironment, options);
        services.AddSingleton(configuration);
        services.AddSingleton(hostEnvironment);

        var engine =
            services.AddSilkyServices<DbMigratorModule>(configuration, hostEnvironment, options);
        var containerBuilder = new ContainerBuilder();
        containerBuilder.Populate(services);
        engine.RegisterDependencies(containerBuilder);
        engine.RegisterModules(containerBuilder);
        var container = containerBuilder.Build();
        engine.ServiceProvider = new AutofacServiceProvider(container);
        return engine;
    }

    private static IConfigurationRoot CreateConfigurationBuilder(IHostEnvironment hostEnvironment,
        SilkyApplicationCreationOptions options)
    {
        return ConfigurationHelper.BuildConfiguration(new ConfigurationBuilder(), hostEnvironment,
            options?.Configuration);
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