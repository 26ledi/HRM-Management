using HRManagement.Exceptions.Shared;

namespace HRManagement.Auth.API.Middleware
{
    /// <summary>
    /// Custom Exception Handler
    /// </summary>
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="GlobalExceptionHandlingMiddleware"/>
        /// </summary>
        /// <param name="next"></param>
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke the middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Write the status and the message of the exception in the body of the response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
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
