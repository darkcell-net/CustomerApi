using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRepository.Entities
{
    public class Customer
    {
        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        [Key]
        public string Id { get; set; }

        public string LastName { get; set; }

        // TODO: Add a state lock property so we can guarantee data integrity when simultaneous updates occur

        public void Update(Customer updateCustomer)
        {
            DateOfBirth = updateCustomer.DateOfBirth;
            FirstName = updateCustomer.FirstName;
            LastName = updateCustomer.LastName;
        }
    }
}