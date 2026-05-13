using RetailSystem.SharedLibrary.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace RetailSystem.API.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var (statusCode, message) = exception switch
            {
                ValidationException => (StatusCodes.Status400BadRequest, exception.Message),
                KeyNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, exception.Message),
                UnauthorizeException => (StatusCodes.Status401Unauthorized, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                success = false,
                statusCode = statusCode,
                message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
