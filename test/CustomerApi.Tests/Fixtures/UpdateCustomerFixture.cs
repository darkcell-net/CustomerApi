using CustomerApi.Commands;
using CustomerApi.Controllers;
using CustomerRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Api = CustomerApi.Models;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Tests.Fixtures
{
    public class UpdateCustomerFixture
    {
        public Entities.Customer[] ExistingCustomers { get; private set; } = { };

        public Entities.Customer[] PostTestCustomers { get; private set; } = { };

        public async Task<IActionResult> UpdateCustomer(Api.Customer customer)
        {
            IServiceProvider provider = TestServiceProvider.CreateProvider();

            using (IServiceScope scope = provider.CreateScope())
            {
                await SeedRepository(scope.ServiceProvider);

                CustomersController controller = scope.ServiceProvider.GetRequiredService<CustomersController>();
                IActionResult result = await controller.UpdateCustomer(customer, scope.ServiceProvider.GetRequiredService<UpdateCustomerCommand>());

                await SnapshotRepository(scope.ServiceProvider);

                return result;
            }
        }

        public UpdateCustomerFixture WithCustomerRepositoryData(Entities.Customer[] repositoryData)
        {
            ExistingCustomers = repositoryData; // TODO should really deep clone the objects

            return this;
        }

        private async Task SeedRepository(IServiceProvider provider)
        {
            CustomerContext context = provider.GetRequiredService<CustomerContext>();

            foreach (Entities.Customer customer in ExistingCustomers)
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