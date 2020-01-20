using CustomerApi.Models;
using CustomerApi.Tests.Fixtures;
using CustomerApi.Tests.TestDoubles;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class When_Adding_A_Customer
    {
        [Fact]
        public async Task The_Customer_Is_Added()
        {
            var customer = new Customer
            {
                FirstName = "Joe",
                Id = Guid.NewGuid().ToString(),
                LastName = "Blogs",
                DateOfBirth = DateTime.UtcNow
            };
            var expectedResponse = new ObjectResult(customer)
            {
                StatusCode = (int)HttpStatusCode.Created
            };

            FakeIdentityGenerator identityGenerator = new FakeIdentityGenerator()
                .WithGeneratedIdentities(customer.Id);
            AddCustomerFixture sut = new AddCustomerFixture()
                .WithIdentityGenerator(identityGenerator);
            IActionResult response = await sut.AddCustomer(customer);

            ((ObjectResult)response).Should().BeEquivalentTo(expectedResponse);
        }
    }
}