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
                .AddMvcCore() // TODO: should really add an exception filter for better formatting errors from the repository
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