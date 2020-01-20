using CustomerApi.Commands;
using CustomerApi.Controllers;
using CustomerRepository;
using CustomerRepository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CustomerApi.Tests.Fixtures
{
    public class DeleteCustomerFixture
    {
        public Customer[] ExistingCustomers { get; private set; } = { };

        public Customer[] PostTestCustomers { get; private set; } = { };

        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            IServiceProvider provider = TestServiceProvider.CreateProvider();

            using (IServiceScope scope = provider.CreateScope())
            {
                await SeedRepository(scope.ServiceProvider);

                CustomersController controller = scope.ServiceProvider.GetRequiredService<CustomersController>();
                IActionResult result = await controller.DeleteCustomer(customerId, scope.ServiceProvider.GetRequiredService<DeleteCustomerCommand>());

                await SnapshotRepository(scope.ServiceProvider);

                return result;
            }
        }

        public DeleteCustomerFixture WithCustomerRepositoryData(Customer[] repositoryData)
        {
            ExistingCustomers = repositoryData; // TODO should really deep clone the objects

            return this;
        }

        private async Task SeedRepository(IServiceProvider provider)
        {
            CustomerContext context = provider.GetRequiredService<CustomerContext>();

            foreach (Customer customer in ExistingCustomers)
            {
                await context.Customers.AddAsync(customer);
            }

            await context.SaveChangesAsync();
        }

        private async Task SnapshotRepository(IServiceProvider provider)
        {
            CustomerContext context = provider.GetRequiredService<CustomerContext>();

            PostTestCustomers = await context.Customers.ToArrayAsync();
        }
    }
}