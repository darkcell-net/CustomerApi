using CustomerApi.Tests.Fixtures;
using CustomerRepository;
using CustomerRepository.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class When_Deleting_A_Customer
    {
        public class And_Customer_Does_Not_Exist
        {
            [Fact]
            public async Task An_Exception_Is_Raised()
            {
                var sut = new DeleteCustomerFixture();
                Func<Task> test = async () => await sut.DeleteCustomer(Guid.NewGuid().ToString());

                await test.Should().ThrowAsync<ResourceNotFoundException>();
            }
        }

        public class And_Customer_Does_Exist
        {
            [Fact]
            public async Task A_Success_Response_Is_Returned()
            {
                string id1 = Guid.NewGuid().ToString();
                string id2 = Guid.NewGuid().ToString();
                const string firstName1 = nameof(firstName1);
                const string firstName2 = nameof(firstName2);
                const string lastName1 = nameof(lastName1);
                const string lastName2 = nameof(lastName2);
                DateTime dateOfBirth1 = DateTime.UtcNow;
                DateTime dateOfBirth2 = dateOfBirth1.AddDays(-1);

                var existingCustomers = new Customer[]
                {
                    new Customer
                    {
                        DateOfBirth = dateOfBirth1,
                        FirstName = firstName1,
                        Id = id1,
                        LastName = lastName1
                    },
                    new Customer
                    {
                        DateOfBirth = dateOfBirth2,
                        FirstName = firstName2,
                        Id = id2,
                        LastName = lastName2
                    },
                };

                DeleteCustomerFixture sut = new DeleteCustomerFixture()
                    .WithCustomerRepositoryData(existingCustomers);
                IActionResult response = await sut.DeleteCustomer(id1);

                response.Should().BeOfType<OkResult>();
            }

            [Fact]
            public async Task Customer_Is_Deleted_From_Datastore()
            {
                string id1 = Guid.NewGuid().ToString();
                string id2 = Guid.NewGuid().ToString();
                const string firstName1 = nameof(firstName1);
                const string firstName2 = nameof(firstName2);
                const string lastName1 = nameof(lastName1);
                const string lastName2 = nameof(lastName2);
                DateTime dateOfBirth1 = DateTime.UtcNow;
                DateTime dateOfBirth2 = dateOfBirth1.AddDays(-1);

                var existingCustomers = new Customer[]
                {
                    new Customer
                    {
                        DateOfBirth = dateOfBirth1,
                        FirstName = firstName1,
                        Id = id1,
                        LastName = lastName1
                    },
                    new Customer
                    {
                        DateOfBirth = dateOfBirth2,
                        FirstName = firstName2,
                        Id = id2,
                        LastName = lastName2
                    },
                };

                DeleteCustomerFixture sut = new DeleteCustomerFixture()
                    .WithCustomerRepositoryData(existingCustomers);
                await sut.DeleteCustomer(id1);

                sut.PostTestCustomers.Should().BeEquivalentTo(existingCustomers[1]);
            }
        }
    }
}