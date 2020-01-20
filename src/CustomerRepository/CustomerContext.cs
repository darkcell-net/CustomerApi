using CustomerRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerRepository
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        { }
    }
}