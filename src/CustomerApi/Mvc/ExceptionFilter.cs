using CustomerApi.Models;
using CustomerRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;

namespace CustomerApi.Mvc
{
    public class ExceptionFilter : IExceptionFilter
    {
        private static readonly MediaTypeCollection ErrorContentType = new MediaTypeCollection { ContentTypes.ErrorVersion1 };

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case MissingRequestBodyException _:
                    context.Result = new BadRequestObjectResult(new CustomerError { Message = "The request body is missing" })
                    {
                        ContentTypes = ErrorContentType
                    };
                    break;

                case ResourceNotFoundException _:
                    context.Result = new NotFoundObjectResult(new CustomerError { Message = "The resource identifier could not be found" })
                    {
                        ContentTypes = ErrorContentType
                    };
                    break;

                default:
                    context.Result = new ObjectResult(new CustomerError { Message = "An unexpected error has occurred" })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        ContentTypes = ErrorContentType
                    };
                    break;
            }
        }
    }
}