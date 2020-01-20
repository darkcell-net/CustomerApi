using CustomerApi.Commands;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    // TODO: add authorisation when required
    // TODO: add exception filter to catch/reformat errors from the repository
    [Route("customers")]
    public class CustomersController : Controller
    {
        /// <summary>
        /// Add a new customer to the datastore
        /// </summary>
        /// <param name="customer">Customer details to add</param>
        /// <param name="command"></param>
        /// <returns>Created customer with its assigned identity</returns>
        /// <response code="201">Returns created customer <see cref="Customer"/></response>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer, [FromServices] AddCustomerCommand command)
        {
            Customer updatedCustomer = await command.Execute(customer);
            ObjectResult result = CreatedAtRoute(new { customerId = updatedCustomer.Id }, updatedCustomer); // TODO: No requirement for a redirect response

            return result;
        }

        /// <summary>
        /// Delete a customer from the datastore
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="customerId">Identity of the customer to delete</param>
        /// <param name="command"></param>
        /// <returns>Ok response</returns>
        /// <response code="200">Customer has been deleted successfully</response>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(string customerId, [FromServices] DeleteCustomerCommand command)
        {
            await command.Execute(customerId);

            return Ok();
        }

        /// <summary>
        /// Searches for customers based on first and/or last name returning any matches
        /// </summary>
        /// <param name="firstName">First name of customers to search for</param>
        /// <param name="lastName">Last name of customers to search for</param>
        /// <param name="command"></param>
        /// <returns>List of any customers matching the search criteria</returns>
        /// <response code="200">List of matching customers <see cref="Customer"/></response>
        /// <response code="404">No matching customers were found</response>
        [HttpGet]
        public async Task<IActionResult> GetCustomers(string firstName, string lastName, [FromServices] GetCustomersCommand command)
        {
            Customer[] customers = await command.Execute(firstName, lastName);

            if (customers.Length == 0)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        /// <summary>
        /// Update a given customer in the datastore
        /// </summary>
        /// <param name="customer">Customer to update <see cref="Customer"/></param>
        /// <param name="command"></param>
        /// <returns>Ok response</returns>
        /// <response code="200">Update was performed successfully</response>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer, [FromServices] UpdateCustomerCommand command)
        {
            await command.Execute(customer);

            return Ok();
        }
    }
}