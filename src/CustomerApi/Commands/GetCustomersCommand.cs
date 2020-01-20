using CustomerApi.Mappers;
using CustomerApi.Models;
using CustomerRepository;
using System.Linq;
using System.Threading.Tasks;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Commands
{
    public class GetCustomersCommand
    {
        private readonly CustomerEntityMapper _mapper;
        private readonly ICustomerRepository _respository;

        public GetCustomersCommand(ICustomerRepository respository, CustomerEntityMapper customerMapper)
        {
            _respository = respository;
            _mapper = customerMapper;
        }

        public async Task<Customer[]> Execute(string firstName, string lastName)
        {
            Entities.Customer[] customers = await _respository.GetCustomers(firstName, lastName);

            return customers.Select(_mapper.Map).ToArray();
        }
    }
}