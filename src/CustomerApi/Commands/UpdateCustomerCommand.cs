using CustomerApi.Mappers;
using CustomerApi.Models;
using CustomerRepository;
using System.Threading.Tasks;

namespace CustomerApi.Commands
{
    public class UpdateCustomerCommand
    {
        private readonly CustomerEntityMapper _mapper;
        private readonly ICustomerRepository _respository;

        public UpdateCustomerCommand(ICustomerRepository respository, CustomerEntityMapper customerMapper)
        {
            _respository = respository;
            _mapper = customerMapper;
        }

        public async Task Execute(Customer customer)
        {
            await _respository.UpdateCustomer(_mapper.Map(customer));
        }
    }
}