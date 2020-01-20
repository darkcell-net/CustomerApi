using CustomerApi.Mvc;
using CustomerApi.Tests.Fixtures;
using CustomerRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;
using Api = CustomerApi.Models;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Tests
{
    public class When_Updating_A_Customer
    {
        public class And_The_Customer_Details_Are_Missing
        {
            [Fact]
            public async Task An_Exception_Is_Raised()
            {
                var sut = new UpdateCustomerFixture();
                Func<Task> test = async () => await sut.UpdateCustomer(null);

                await test.Should().ThrowAsync<MissingRequestBodyException>();
            }
        }

        public class And_The_Customer_Details_Are_Invalid
        {
            [Fact]
            public async Task An_Unprocessible_Entity_Result_Is_Returned()
            {
                var sut = new UpdateCustomerFixture();
                IActionResult response = await sut.UpdateCustomer(new Api.Customer());

                response.Should().BeOfType<UnprocessableEntityResult>();
            }
        }

        public class And_The_Customer_Does_Not_Exist
        {
            [Fact]
            public async Task An_Exception_Is_Raised()
            {
                string id = Guid.NewGuid().ToString();
                const string firstName = nameof(firstName);
                const string lastName = nameof(lastName);
                DateTime dateOfBirth = DateTime.UtcNow;

                var customer = new Api.Customer
                {
                    DateOfBirth = dateOfBirth,
                    FirstName = firstName,
                    Id = id,
                    LastName = lastName
                };

                var sut = new UpdateCustomerFixture();
                Func<Task> test = async () => await sut.UpdateCustomer(customer);

                await test.Should().ThrowAsync<ResourceNotFoundException>();
            }
        }

        public class And_The_Customer_Does_Exist
        {
            [Fact]
            public async Task A_Success_Response_Is_Returned()
            {
                string id1 = Guid.NewGuid().ToString();
                string id2 = Guid.NewGuid().ToString();
                const string firstName1 = nameof(firstName1);
                const string firstName1a = nameof(firstName1a);
                const string firstName2 = nameof(firstName2);
                const string lastName1 = nameof(lastName1);
                const string lastName1a = nameof(lastName1a);
                const string lastName2 = nameof(lastName2);
                DateTime dateOfBirth1 = DateTime.UtcNow;
                DateTime dateOfBirth1a = dateOfBirth1.AddDays(-1);
                DateTime dateOfBirth2 = dateOfBirth1.AddDays(-2);

                var existingCustomers = new Entities.Customer[]
                {
                    new Entities.Customer
                    {
                        DateOfBirth = dateOfBirth1,
                        FirstName = firstName1,
                        Id = id1,
                        LastName = lastName1
                    },
                    new Entities.Customer
                    {
                        DateOfBirth = dateOfBirth2,
                        FirstName = firstName2,
                        Id = id2,
                        LastName = lastName2
                    },
                };

                var updateCustomer = new Api.Customer
                {
                    DateOfBirth = dateOfBirth1a,
                    FirstName = firstName1a,
                    Id = id1,
                    LastName = lastName1a
                };

                UpdateCustomerFixture sut = new UpdateCustomerFixture()
                    .WithCustomerRepositoryData(existingCustomers);
                IActionResult response = await sut.UpdateCustomer(updateCustomer);

                response.Should().BeOfType<OkResult>();
            }

            [Fact]
            public async Task Customer_Is_Updated_In_The_Datastore()
            {
                string id1 = Guid.NewGuid().ToString();
                string id2 = Guid.NewGuid().ToString();
                const string firstName1 = nameof(firstName1);
                const string firstName1a = nameof(firstName1a);
                const string firstName2 = nameof(firstName2);
                const string lastName1 = nameof(lastName1);
                const string lastName1a = nameof(lastName1a);
                const string lastName2 = nameof(lastName2);
                DateTime dateOfBirth1 = DateTime.UtcNow;
                DateTime dateOfBirth1a = dateOfBirth1.AddDays(-1);
                DateTime dateOfBirth2 = dateOfBirth1.AddDays(-2);

                var existingCustomers = new Entities.Customer[]
                {
                    new Entities.Customer
                    {
                        DateOfBirth = dateOfBirth1,
                        FirstName = firstName1,
                        Id = id1,
                        LastName = lastName1
                    },
                    new Entities.Customer
                    {
                        DateOfBirth = dateOfBirth2,
                        FirstName = firstName2,
                        Id = id2,
                        LastName = lastName2
                    },
                };
                var expectedCustomers = new Entities.Customer[]
                {
                    new Entities.Customer
                    {
                        DateOfBirth = dateOfBirth1a,
                        FirstName = firstName1a,
                        Id = id1,
                        LastName = lastName1a
                    },
                    new Entities.Customer
                    {
                        DateOfBirth = dateOfBirth2,
                        FirstName = firstName2,
                        Id = id2,
                        LastName = lastName2
                    },
                };

                var updateCustomer = new Api.Customer
                {
                    DateOfBirth = dateOfBirth1a,
                    FirstName = firstName1a,
                    Id = id1,
                    LastName = lastName1a
                };

                UpdateCustomerFixture sut = new UpdateCustomerFixture()
                    .WithCustomerRepositoryData(existingCustomers);
                await sut.UpdateCustomer(updateCustomer);

                sut.PostTestCustomers.Should().BeEquivalentTo(expectedCustomers);
            }
        }
    }
}