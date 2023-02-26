using DroneManager.Core.Exceptions;
using DroneManager.Core.Extensions;
using DroneManager.Core.Wrappers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace DroneManager.Core.Web.Middlewares
{
    /// <summary>
    /// Catch flow errors.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = exception switch
                {
                    // custom application error
                    ApiException => (int)HttpStatusCode.BadRequest,
                    // custom validation error
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    // unhandled error
                    _ => (int)HttpStatusCode.InternalServerError
                };

                IBaseResponse responseModel = exception is ValidationException validationException ?
                    new ValidationResponse(response.StatusCode, exception.Message, validationException) :
                    new Response<string>()
                    {
                        Code = response.StatusCode,
                        Succeeded = false,
                        Message = exception.Message,
                        Errors = exception.GetAllMessages().ToList()
                    };

                var result = JsonConvert.SerializeObject(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
