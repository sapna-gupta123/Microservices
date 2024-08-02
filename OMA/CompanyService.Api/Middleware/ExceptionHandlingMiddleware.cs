using SharedService;
using System.Net;
using System.Text.Json;

namespace CompanyService.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // Customize response based on exception type
            response.StatusCode = exception switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var apiResponse = new ApiResponse<object>
            {
                Status = "Error",
                Code = response.StatusCode,
                Message = exception.Message,
                Data = null // or additional data about the error if needed
            };

            var result = JsonSerializer.Serialize(apiResponse);
            return response.WriteAsync(result);
        }
    }
}
