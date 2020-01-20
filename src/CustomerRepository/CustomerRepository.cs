using CustomerRepository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _customerContext;
        private readonly IIdentityGenerator _identityGenerator;

        public CustomerRepository(CustomerContext context, IIdentityGenerator identityGenerator)
        {
            _customerContext = context;
            _identityGenerator = identityGenerator;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            customer.Id = _identityGenerator.GenerateId();

            await _customerContext.Customers.AddAsync(customer);

            await _customerContext.SaveChangesAsync();

            return customer;
        }

        public async Task DeleteCustomer(string customerId)
        {
            Customer customer = await _customerContext.Customers.FindAsync(customerId);

            _customerContext.Customers.Remove(customer);

            await _customerContext.SaveChangesAsync();
        }

        public async Task<Customer[]> GetCustomers(string firstName, string lastName)
        {
            // TODO: should limit the number of records brought back
            return await _customerContext.Customers
                .Where(c => c.FirstName == firstName || c.LastName == lastName)
                .ToArrayAsync();
        }

        public async Task UpdateCustomer(Customer updateCustomer)
        {
            Customer customer = await _customerContext.Customers.FindAsync(updateCustomer.Id);

            customer.Update(updateCustomer);

            await _customerContext.SaveChangesAsync();
        }
    }
}