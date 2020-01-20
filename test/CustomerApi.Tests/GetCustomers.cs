using CustomerApi.Models;
using CustomerApi.Tests.Fixtures;
using CustomerApi.Tests.TheoryData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    public class When_Searching_For_A_Customer
    {
        public class And_Customer_Does_Not_Exist
        {
            [Fact]
            public async Task An_Error_Is_Returned()
            {
                var sut = new GetCustomersFixture();
                var response = (IStatusCodeActionResult)await sut.GetCustomers("Joe", "Blogs");

                response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            }
        }

        public class And_Customers_Do_Exist
        {
            [Theory, ClassData(typeof(GetCustomersTheories))]
            public async Task The_Customers_Are_Returned(GetCustomersTheoryData theoryData)
            {
                var expectedResponse = new ObjectResult(new[] { theoryData.ExpectedResponse })
                {
                    StatusCode = (int)HttpStatusCode.OK
                };

                GetCustomersFixture sut = new GetCustomersFixture()
                    .WithCustomerRepositoryData(theoryData.ExistingCustomers);
                IActionResult response = await sut.GetCustomers(theoryData.FirstName, theoryData.LastName);

                ((ObjectResult)response).StatusCode.Should().Be(expectedResponse.StatusCode);
                ((ObjectResult)response).Value.GetType().Should().Be(theoryData.ExpectedResponse.GetType());
                ((Customer[])((ObjectResult)response).Value).Should().BeEquivalentTo(theoryData.ExpectedResponse, options => options.Excluding(c => c.Id));
            }
        }
    }
}