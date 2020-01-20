using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerRepository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services
                .AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase(nameof(CustomerRepository)))
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddSingleton<IIdentityGenerator, IdentityGenerator>();
        }
    }
}