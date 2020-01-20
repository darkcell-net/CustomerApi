using Microsoft.Extensions.DependencyInjection;

namespace CustomerApi.Commands
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services
                .AddScoped<AddCustomerCommand>()
                .AddScoped<DeleteCustomerCommand>()
                .AddScoped<GetCustomersCommand>()
                .AddScoped<UpdateCustomerCommand>();
        }
    }
}