using CustomerApi.Commands;
using CustomerApi.Controllers;
using CustomerApi.Models;
using CustomerApi.Tests.TestDoubles;
using CustomerRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Tests.Fixtures
{
    public class AddCustomerFixture
    {
        private Action<IServiceCollection> _setup = _ => { };

        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            IServiceProvider provider = TestServiceProvider.CreateProvider(_setup);

            using (IServiceScope scope = provider.CreateScope())
            {
                CustomersController controller = scope.ServiceProvider.GetRequiredService<CustomersController>();

                return await controller.AddCustomer(customer, scope.ServiceProvider.GetRequiredService<AddCustomerCommand>());
            }
        }

        public AddCustomerFixture WithIdentityGenerator(IIdentityGenerator generator)
        {
            _setup += services => services.AddIdentityGenerator(generator);

            return this;
        }
    }
}