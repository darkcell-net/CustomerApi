using CustomerRepository;
using System.Threading.Tasks;

namespace CustomerApi.Commands
{
    public class DeleteCustomerCommand
    {
        private readonly ICustomerRepository _respository;

        public DeleteCustomerCommand(ICustomerRepository respository)
        {
            _respository = respository;
        }

        public async Task Execute(string customerId)
        {
            await _respository.DeleteCustomer(customerId);
        }
    }
}