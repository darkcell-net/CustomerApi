using Api = CustomerApi.Models;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Mappers
{
    public class CustomerEntityMapper
    {
        public Entities.Customer Map(Api.Customer customer)
        {
            return new Entities.Customer
            {
                DateOfBirth = customer.DateOfBirth.Value, // TODO: add test to check for a null DateOfBirth when adding new customer
                FirstName = customer.FirstName,
                Id = customer.Id,
                LastName = customer.LastName
            };
        }

        public Api.Customer Map(Entities.Customer customer)
        {
            return new Api.Customer
            {
                DateOfBirth = customer.DateOfBirth,
                FirstName = customer.FirstName,
                Id = customer.Id,
                LastName = customer.LastName
            };
        }
    }
}