using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Mappers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddSingleton<CustomerEntityMapper>();

            return services;
        }
    }
}