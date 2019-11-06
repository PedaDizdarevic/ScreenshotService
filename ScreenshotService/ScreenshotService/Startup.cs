using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ScreenshotService.Drivers;
using System;
using System.IO;
using System.Reflection;

namespace ScreenshotService
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
            services.AddControllers();
            services.AddScoped<IBrowserDriver, BrowserDriver>();
            services.AddSingleton(Configuration);

            var usingInMemoryDatabase = Configuration["UsingInMemoryDatabase"];

            if (bool.Parse(usingInMemoryDatabase))
            {
                services.AddSingleton<IScreenshotRepository, ScreenshotInMemoryRepository>();
            }
            else
            {
                DatabaseUpdateCheck(Configuration["ConnectionString"]);

                services.AddScoped<IScreenshotRepository, ScreenshotRepository>();
            }

            SwaggerSetup(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SwaggerUISetup(app);
        }

        private void DatabaseUpdateCheck(string connectionStringArg)
        {
            var dbDeployer = new DbDeployer(connectionStringArg);
            var isSuccess = dbDeployer.Deploy();

            if (!isSuccess)
            {
                throw new Exception("Database update check failed.");
            }
        }

        private void SwaggerSetup(IServiceCollection servicesArg)
        {
            servicesArg.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScreenshotService", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory + "/" + Assembly.GetExecutingAssembly().GetName().Name + ".xml");

                c.IncludeXmlComments(filePath);
            });
        }

        private void SwaggerUISetup(IApplicationBuilder applicationBuilderArg)
        {
            applicationBuilderArg.UseSwagger();
            applicationBuilderArg.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScreenshotService");
            });
        }
    }
}
