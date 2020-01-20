using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerRepository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string databaseName)
        {
            return services
                .AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase(databaseName))
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddSingleton<IIdentityGenerator, IdentityGenerator>();
        }
    }
}