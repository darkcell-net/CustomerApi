using CustomerRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CustomerApi.Tests.TestDoubles
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityGenerator(this IServiceCollection services, IIdentityGenerator identityGenerator)
        {
            return services.Replace(ServiceDescriptor.Singleton(identityGenerator));
        }
    }
}