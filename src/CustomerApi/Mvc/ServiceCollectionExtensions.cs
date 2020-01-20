using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CustomerApi.Mvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services
                .AddOptions()
                .AddMvcCore(options => options.Filters.Add(typeof(ExceptionFilter)))
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddJsonFormatters(
                    settings =>
                    {
                        settings.NullValueHandling = NullValueHandling.Ignore;
                    });

            return services;
        }
    }
}