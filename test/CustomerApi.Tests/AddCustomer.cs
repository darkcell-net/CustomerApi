using CustomerApi.Mvc;
using CustomerApi.Tests.Fixtures;
using CustomerApi.Tests.TestDoubles;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Threading.Tasks;
using Xunit;
using Api = CustomerApi.Models;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Tests
{
    public class When_Adding_A_Customer
    {
        public class And_The_Customer_Details_Are_Missing
        {
            [Fact]
            public async Task An_Exception_Is_Raised()
            {
                var sut = new AddCustomerFixture();
                Func<Task> test = async () => await sut.AddCustomer(null);

                await test.Should().ThrowAsync<MissingRequestBodyException>();
            }
        }

        public class And_The_Customer_Details_Are_Invalid
        {
            [Fact]
            public async Task An_Unprocessible_Entity_Result_Is_Returned()
            {
                var sut = new AddCustomerFixture();
                IActionResult response = await sut.AddCustomer(new Api.Customer());

                response.Should().BeOfType<UnprocessableEntityResult>();
            }
        }

        public class And_The_Request_Is_Valid
        {
            [Fact]
            public async Task A_Success_Response_Is_Returned()
            {
                var customer = new Api.Customer
                {
                    FirstName = "Joe",
                    Id = Guid.NewGuid().ToString(),
                    LastName = "Blogs",
                    DateOfBirth = DateTime.UtcNow
                };
                var expectedResponse = new CreatedAtRouteResult(new { customerId = customer.Id }, customer)
                {
                    ContentTypes = new MediaTypeCollection { ContentTypes.CustomerVersion1 }
                };

                FakeIdentityGenerator identityGenerator = new FakeIdentityGenerator()
                    .WithGeneratedIdentities(customer.Id);
                AddCustomerFixture sut = new AddCustomerFixture()
                    .WithIdentityGenerator(identityGenerator);
                IActionResult response = await sut.AddCustomer(customer);

                ((ObjectResult)response).Should().BeEquivalentTo(expectedResponse);
            }

            [Fact]
            public async Task The_Customer_Is_Added_To_The_Datastore()
            {
                string id = Guid.NewGuid().ToString();
                const string firstName = nameof(firstName);
                const string lastName = nameof(lastName);
                DateTime dateOfBirth = DateTime.UtcNow;

                var customer = new Api.Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth
                };
                var expectedCustomers = new Entities.Customer[]
                {
                    new Entities.Customer
                    {
                        DateOfBirth = dateOfBirth,
                        FirstName = firstName,
                        Id = id,
                        LastName = lastName
                    }
                };

                FakeIdentityGenerator identityGenerator = new FakeIdentityGenerator()
                    .WithGeneratedIdentities(id);
                AddCustomerFixture sut = new AddCustomerFixture()
                    .WithIdentityGenerator(identityGenerator);
                await sut.AddCustomer(customer);

                sut.PostTestCustomers.Should().BeEquivalentTo(expectedCustomers);
            }
        }
    }
}