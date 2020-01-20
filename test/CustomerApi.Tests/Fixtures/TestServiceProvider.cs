using CustomerApi.Commands;
using CustomerApi.Controllers;
using CustomerApi.Mappers;
using CustomerApi.Mvc;
using CustomerRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Tests.Fixtures
{
    public class TestServiceProvider
    {
        public static IActionContextAccessor CreateActionContextAccessor(IServiceProvider provider)
        {
            var httpContext = new DefaultHttpContext();
            var routeData = new RouteData();
            var actionContext = new ActionContext(httpContext, routeData, new ControllerActionDescriptor());
            var routeBuilder = new RouteBuilder(new ApplicationBuilder(provider))
            {
                DefaultHandler = new RouteHandler(context => Task.CompletedTask)
            };

            httpContext.RequestServices = provider;

            routeBuilder.MapRoute(string.Empty, "{controller}/{action}/{id}", new RouteValueDictionary(new { id = "defaultid" }));
            routeData.Routers.Add(routeBuilder.Build());

            return new ActionContextAccessor { ActionContext = actionContext };
        }

        public static IServiceProvider CreateProvider(Action<IServiceCollection> overrides = null)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();

            IServiceCollection services = new ServiceCollection()
                .AddLogging()
                .AddMvcServices()
                .AddCommands()
                .AddMappers()
                .AddRepository(Guid.NewGuid().ToString())
                .AddTransient(
                    provider => new CustomersController()
                    {
                        ControllerContext = new ControllerContext(provider.GetRequiredService<IActionContextAccessor>().ActionContext)
                    })
                .Replace(ServiceDescriptor.Singleton(provider => CreateActionContextAccessor(provider)))
                .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            overrides?.Invoke(services);

            return services.BuildServiceProvider(true);
        }
    }
}