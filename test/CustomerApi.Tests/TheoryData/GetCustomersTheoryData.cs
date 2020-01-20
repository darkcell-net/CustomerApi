using Api = CustomerApi.Models;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Tests.TheoryData
{
    public class GetCustomersTheoryData
    {
        public Entities.Customer[] ExistingCustomers { get; set; } = { };

        public Api.Customer[] ExpectedResponse { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}