using ScalableApplication.Application.DTOs.Employee;
using ScalableApplication.Application.Exceptions;
using System.Net;

namespace ScalableApplication.API.Middleware
{
    public class GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ResourceNotFoundException ef)
            {
                await HandleExceptionAsync(context, ef, HttpStatusCode.NotFound);
            }
            catch(ArgumentException argEx)
            {
                await HandleExceptionAsync(context, argEx, HttpStatusCode.BadRequest);
            }
            catch (InvalidOperationException ioe)
            {
                await HandleExceptionAsync(context, ioe);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            CustomHttpResponse<string> response = new(statusCode, null, exception.Message);

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
