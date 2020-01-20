using CustomerApi.Mvc;
using CustomerRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CustomerApi.Tests
{
    [Route(Route)]
    public class ExceptionController : Controller
    {
        public const string Route = "D20E232C-7E19-4F35-A623-08CFF9A3FC1B";

        [HttpGet(nameof(ThrowMissingRequestBodyException))]
        public void ThrowMissingRequestBodyException()
        {
            throw new MissingRequestBodyException();
        }

        [HttpGet(nameof(ThrowResourceNotFoundException))]
        public void ThrowResourceNotFoundException()
        {
            throw new ResourceNotFoundException(nameof(ExceptionController));
        }

        [HttpGet(nameof(ThrowUnexpectedException))]
        public void ThrowUnexpectedException()
        {
            throw new UnexpectedException();
        }
    }

    public class UnexpectedException : Exception { }

    public class When_An_Exception_Is_Thrown
    {
        [Theory]
        [InlineData(nameof(ExceptionController.ThrowMissingRequestBodyException), "The request body is missing", HttpStatusCode.BadRequest)]
        [InlineData(nameof(ExceptionController.ThrowResourceNotFoundException), "The resource identifier could not be found", HttpStatusCode.NotFound)]
        [InlineData(nameof(ExceptionController.ThrowUnexpectedException), "An unexpected error has occurred", HttpStatusCode.InternalServerError)]
        public async Task A_Formatted_Response_Is_Returned(string action, string errorMessage, HttpStatusCode responseStatus)
        {
            var expectedResponse = JObject.FromObject(new { message = errorMessage });

            HttpResponseMessage response = await PerformRequest(action);
            var actualResponse = JObject.Parse(await response.Content.ReadAsStringAsync());

            response.StatusCode.Should().Be(responseStatus);
            response.Content.Headers.ContentType.MediaType.Should().Be(ContentTypes.ErrorVersion1);
            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        private TestServer CreateServer() =>
            new TestServer(
                new WebHostBuilder()
                    .Configure(app => app.UseMvc())
                    .ConfigureServices(services => services.AddMvcServices()));

        private async Task<HttpResponseMessage> PerformRequest(string action)
        {
            using (TestServer server = CreateServer())
            {
                using (HttpClient client = server.CreateClient())
                {
                    return await client.GetAsync($"http://localhost/{ExceptionController.Route}/{action}");
                }
            }
        }
    }
}