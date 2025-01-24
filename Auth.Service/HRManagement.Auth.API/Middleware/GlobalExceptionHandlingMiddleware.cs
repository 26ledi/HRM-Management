using HRManagement.Exceptions.Shared;

namespace HRManagement.Auth.API.Middleware
{

    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            catch (NotFoundException ex)
            {
                await WriteExceptionMessageAndStatusCode(context, StatusCodes.Status404NotFound, ex);
            }
            catch (AlreadyExistsException ex)
            {
                await WriteExceptionMessageAndStatusCode(context, StatusCodes.Status409Conflict, ex);
            }
            catch (Exception ex)
            {
                await WriteExceptionMessageAndStatusCode(context, StatusCodes.Status500InternalServerError, ex);
            }
        }

        private async Task WriteExceptionMessageAndStatusCode(HttpContext context, int statusCode, Exception ex)
        {
            _logger.LogError(0, ex, "{message}", ex.Message);
            context.Response.StatusCode = statusCode;

            if (statusCode.Equals(500))
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = statusCode,
                    TraceId = ex.HResult.ToString(),
                    Message = "Internal Server Error",
                });
            }
            else
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = statusCode,
                    TraceId = ex.HResult.ToString(),
                    Message = ex.Message,
                });
            }
        }
    }
}
