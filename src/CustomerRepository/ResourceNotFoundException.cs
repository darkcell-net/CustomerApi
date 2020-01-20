using CustomerRepository.Entities;
using System;

namespace CustomerRepository
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string customerId) : this(null, customerId)
        {
        }

        public ResourceNotFoundException(Exception innerException, string customerId)
            : base($"An error occurred when retrieving a customer with {nameof(Customer.Id)} {customerId}", innerException)
        {
        }
    }
}