using CustomerApi.Mappers;
using CustomerApi.Models;
using CustomerRepository;
using System.Threading.Tasks;

namespace CustomerApi.Commands
{
    public class AddCustomerCommand
    {
        private readonly CustomerEntityMapper _mapper;
        private readonly ICustomerRepository _respository;

        public AddCustomerCommand(ICustomerRepository respository, CustomerEntityMapper customerMapper)
        {
            _respository = respository;
            _mapper = customerMapper;
        }

        public async Task<Customer> Execute(Customer customer)
        {
            return _mapper.Map(await _respository.AddCustomer(_mapper.Map(customer)));
        }
    }
}