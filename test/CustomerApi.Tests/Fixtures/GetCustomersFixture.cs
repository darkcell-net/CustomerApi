using CustomerApi.Commands;
using CustomerApi.Controllers;
using CustomerRepository;
using CustomerRepository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Tests.Fixtures
{
    public class GetCustomersFixture
    {
        private Customer[] _existingCusomers = { };

        public async Task<IActionResult> GetCustomers(string firstName, string lastName)
        {
            IServiceProvider provider = TestServiceProvider.CreateProvider();

            using (IServiceScope scope = provider.CreateScope())
            {
                await SeedRepository(scope.ServiceProvider);

                CustomersController controller = scope.ServiceProvider.GetRequiredService<CustomersController>();

                return await controller.GetCustomers(firstName, lastName, scope.ServiceProvider.GetRequiredService<GetCustomersCommand>());
            }
        }

        public GetCustomersFixture WithCustomerRepositoryData(Customer[] repositoryData)
        {
            _existingCusomers = repositoryData; // TODO should really deep clone the objects

            return this;
        }

        private async Task SeedRepository(IServiceProvider provider)
        {
            CustomerContext context = provider.GetRequiredService<CustomerContext>();

            foreach (Customer customer in _existingCusomers)
            {
                await context.Customers.AddAsync(customer);
            }

            await context.SaveChangesAsync();
        }
    }
}