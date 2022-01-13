using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silky.Core.Extensions;
using Silky.Log.Domain.AuditLogging.Events.Consumers;
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

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration["rabbitMq:host"],
                        configuration["rabbitMq:port"].To<ushort>(),
                        configuration["rabbitMq:virtualHost"],
                        config =>
                        {
                            config.Username(configuration["rabbitMq:userName"]);
                            config.Password(configuration["rabbitMq:password"]);
                        });
                    configurator.ReceiveEndpoint("auditlog-events-listener",
                        e => { e.Consumer<AuditLogEventConsumer>(); });
                });
                services.AddMassTransitHostedService();
            });

            services.AddDatabaseAccessor(
                options => { options.AddDbPool<DefaultContext>(); },
                "Silky.Log.Database.Migrations");
        }
    }
}