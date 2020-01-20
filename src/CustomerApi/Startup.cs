using CustomerApi.Commands;
using CustomerApi.Mappers;
using CustomerApi.Mvc;
using CustomerRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace CustomerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder appBuilder, IHostingEnvironment env)
        {
            appBuilder
                .UseSwagger()
                .UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API v1");
                        c.RoutePrefix = string.Empty;
                    });

            appBuilder.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcServices()
                .AddCommands()
                .AddMappers()
                .AddRepository();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Title = "Customer API",
                        Version = "v1"
                    });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}