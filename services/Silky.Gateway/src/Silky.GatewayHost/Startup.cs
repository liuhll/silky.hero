using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silky.GatewayHost.AuditLogging;
using Silky.GatewayHost.Authorization;
using Silky.Hero.Common;
using Silky.Http.Core;
using Silky.Http.MiniProfiler;

namespace Silky.GatewayHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSilkyHttpCore()
                .AddDashboard()
                .AddResponseCaching()
                .AddHttpContextAccessor()
                .AddRouting()
                .AddSilkyIdentity<AuthorizationHandler>()
                .AddSilkyMiniProfiler()
                .AddSwaggerDocuments()
                .AddAuditing<HeroAuditingStore>();

            services.AddCorsAccessor();

            services.AddHealthChecks()
                .AddSilkyRpc()
                .AddSilkyGateway()
                ;
            services
                .AddHealthChecksUI()
                .AddInMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment(SilkyHeroConsts.DemoEnvironment))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocuments();
                app.UseMiniProfiler();
            }

            app.UseDashboard();
            app.UseSilkyRpcHealthCheck()
                .UseSilkyGatewayHealthCheck()
                .UseHealthChecksPrometheusExporter("/metrics");
            app.UseRouting();
            app.UseCorsAccessor();
            app.UseResponseCaching();
            app.UseSilkyWebSocketsProxy();
            app.UseSilkyWrapperResponse();
            app.UseSilkyIdentity();
            app.UseSilkyWebServer();
            app.UseAuditing();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecksUI();
                endpoints.MapSilkyRpcServices();
            });
        }
    }
}