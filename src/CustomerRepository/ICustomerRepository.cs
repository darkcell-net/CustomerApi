using CustomerRepository.Entities;
using System.Threading.Tasks;

namespace CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomer(Customer customer);

        Task DeleteCustomer(string customerId);

        Task<Customer[]> GetCustomers(string firstName, string lastName);

        Task UpdateCustomer(Customer customer);
    }
}