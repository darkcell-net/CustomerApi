using CustomerApi.Commands;
using CustomerApi.Controllers;
using CustomerApi.Models;
using CustomerApi.Tests.TestDoubles;
using CustomerRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Tests.Fixtures
{
    public class AddCustomerFixture
    {
        private Action<IServiceCollection> _setup = _ => { };

        public Entities.Customer[] PostTestCustomers { get; private set; } = { };

        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            IServiceProvider provider = TestServiceProvider.CreateProvider(_setup);

            using (IServiceScope scope = provider.CreateScope())
            {
                CustomersController controller = scope.ServiceProvider.GetRequiredService<CustomersController>();
                IActionResult result = await controller.AddCustomer(customer, scope.ServiceProvider.GetRequiredService<AddCustomerCommand>());

                await SnapshotRepository(scope.ServiceProvider);

                return result;
            }
        }

        public AddCustomerFixture WithIdentityGenerator(IIdentityGenerator generator)
        {
            _setup += services => services.AddIdentityGenerator(generator);

            return this;
        }

        private async Task SnapshotRepository(IServiceProvider provider)
        {
            CustomerContext context = provider.GetRequiredService<CustomerContext>();

            PostTestCustomers = await context.Customers.ToArrayAsync();
        }
    }
}