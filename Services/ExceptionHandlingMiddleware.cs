using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Employee_Management.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException notFoundException)
            {
                httpContext.Response.StatusCode = notFoundException.StatusCode;
                httpContext.Response.ContentType = "application/json";
                var responseMessage = new
                {
                    message = notFoundException.Message
                };
                await httpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(responseMessage));

            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                var responseMessage = new
                {
                    message = ex.Message
                };
                await httpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(responseMessage));
            }
        }
        private static async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            Console.WriteLine($"NotFoundException: {exception.Message}");

            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync($"Not Found: {exception.Message}");
        }

        private static async Task HandleInternalServerErrorAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine($"Internal Server Error: {exception.Message}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"An error occured: {exception.Message}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
