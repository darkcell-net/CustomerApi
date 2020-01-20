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
                DateOfBirth = customer.DateOfBirth.Value,
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