namespace GusMelfordBot.Core
{
    using Settings;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices(Configuration);
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            ILogger<Startup> logger,
            CommonSettings commonSettings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"GusMelfordBot.Core v{commonSettings.Version}");
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            logger.LogInformation("TBot started. Time: {Time}", DateTime.UtcNow);
        }
    }
}