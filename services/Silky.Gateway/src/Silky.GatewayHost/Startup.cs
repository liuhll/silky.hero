using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                .AddSilkyIdentity()
                .AddSilkyMiniProfiler()
                .AddSwaggerDocuments();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocuments();
                app.UseMiniProfiler();
            }
            app.UseDashboard();
            app.UseRouting();
            app.UseSilkyWrapperResponse();
            app.UseResponseCaching();
            app.UseSilkyWebSocketsProxy();
            app.UseSilkyIdentity();
            app.UseSilkyHttpServer();           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSilkyRpcServices();
            });
        }
    }
}